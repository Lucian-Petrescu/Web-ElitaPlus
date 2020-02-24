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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
            If Me.Page.StateSession.Item(Me.UniqueID) Is Nothing Then
                Me.Page.StateSession.Item(Me.UniqueID) = New MyState
            End If
            Return CType(Me.Page.StateSession.Item(Me.UniqueID), MyState)
        End Get
    End Property

    Private _validateBankInfoCountry As String
    Public Property ValidateBankInfoCountry() As String
        Get
            Return _validateBankInfoCountry
        End Get
        Set(ByVal value As String)
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
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
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
        If labelNameonAccount.Text.IndexOf("*") <> 0 Then Me.labelNameonAccount.Text = "* " & Me.labelNameonAccount.Text
        If labelBankID.Text.IndexOf("*") <> 0 Then Me.labelBankID.Text = "* " & Me.labelBankID.Text
        If labelBankAccountNo.Text.IndexOf("*") <> 0 Then Me.labelBankAccountNo.Text = "* " & Me.labelBankAccountNo.Text
        If labelCountryOfBank.Text.IndexOf("*") <> 0 Then Me.labelCountryOfBank.Text = "* " & Me.labelCountryOfBank.Text
        If labelSwiftCode.Text.IndexOf("*") <> 0 Then Me.labelSwiftCode.Text = "* " & Me.labelSwiftCode.Text
        If labelIBAN_Number.Text.IndexOf("*") <> 0 Then Me.labelIBAN_Number.Text = "* " & Me.labelIBAN_Number.Text
        If lblBranchDigit.Text.IndexOf("*") <> 0 Then Me.lblBranchDigit.Text = "* " & Me.lblBranchDigit.Text
        If lblAcctDigit.Text.IndexOf("*") <> 0 Then Me.lblAcctDigit.Text = "* " & Me.lblAcctDigit.Text
        If lblBranchNumber.Text.IndexOf("*") <> 0 Then Me.lblBranchNumber.Text = "* " & Me.lblBranchNumber.Text
        If lblBankName.Text.IndexOf("*") <> 0 Then Me.lblBankName.Text = "* " & Me.lblBankName.Text
        If labelAccountType.Text.IndexOf("*") <> 0 Then Me.labelAccountType.Text = "* " & Me.labelAccountType.Text

    End Sub



    Public Sub Bind(ByVal bankinfoBo As BankInfo)
        With State
            .myBankInfoBo = bankinfoBo
        End With
        Me.textboxBankID.Text = String.Empty
        Me.textboxBankAccountNo.Text = String.Empty

        ' SwitchToLastFourDigitsLabelMode(Me.State.myBankInfoBo.ValidateFieldsforFR)


        If Me.State.myBankInfoBo.ValidateFieldsforFR Then
            EnableDisableRequiredControls()
        Else

            'If (Not (Me.State.myBankInfoBo.SourceCountryID.Equals(Guid.Empty))) AndAlso (Not (Me.Page.GetSelectedItem(Me.moCountryDrop_WRITE).Equals(Guid.Empty))) Then --req-5383
            If (Not (Me.State.myBankInfoBo.SourceCountryID.Equals(Guid.Empty))) AndAlso (Not (Me.State.myBankInfoBo.CountryID.Equals(Guid.Empty))) Then
                If Me.State.myBankInfoBo.SourceCountryID.Equals(Me.State.myBankInfoBo.CountryID) Then
                    'Domestic transfer
                    DomesticTransfer()
                Else
                    Dim objCountry As New Country(Me.State.myBankInfoBo.CountryID)
                    If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objCountry.EuropeanCountryId) = Codes.YESNO_Y Then
                        'International transfer & Destination is European country
                        InternationalEUTransfer()
                    Else
                        'International transfer & Destination is not a European country
                        InternationalTransfer()
                    End If
                End If
                EnableControlsBasedOnCountry(Me.State.myBankInfoBo.CountryID)
            Else
                'Domestic transfer
                DomesticTransfer()
            End If
        End If
        Me.PopulateControlFromBo()

    End Sub

    Public Sub ReAssignTabIndex(Optional ByVal TabIndexStartingNumber As Int16 = 0)
        If TabIndexStartingNumber > 0 Then
            Me.textboxNameAccount.TabIndex = TabIndexStartingNumber
            Me.moCountryDrop_WRITE.TabIndex = CType(TabIndexStartingNumber + 1, Int16)
            Me.txtBankName.TabIndex = CType(TabIndexStartingNumber + 2, Int16)
            Me.txtBankBranchName.TabIndex = CType(TabIndexStartingNumber + 3, Int16)

            Me.textboxBankID.TabIndex = CType(TabIndexStartingNumber + 4, Int16)
            Me.txtBranchNumber.TabIndex = CType(TabIndexStartingNumber + 5, Int16)
            Me.txtBranchDigit.TabIndex = CType(TabIndexStartingNumber + 6, Int16)

            Me.textboxBankAccountNo.TabIndex = CType(TabIndexStartingNumber + 7, Int16)
            Me.moAccountTypeDrop.TabIndex = CType(TabIndexStartingNumber + 8, Int16)
            Me.txtAcctDigit.TabIndex = CType(TabIndexStartingNumber + 9, Int16)

            ' Me.txtBranchNumber.TabIndex = CType(TabIndexStartingNumber + 8, Int16)

            Me.textboxSwiftCode.TabIndex = CType(TabIndexStartingNumber + 10, Int16)
            Me.textboxIBAN_Number.TabIndex = CType(TabIndexStartingNumber + 11, Int16)
            Me.txtBankLookupCode.TabIndex = CType(TabIndexStartingNumber + 12, Int16)
            Me.txtBankSubcode.TabIndex = CType(TabIndexStartingNumber + 13, Int16)
            Me.txtBankSortCode.TabIndex = CType(TabIndexStartingNumber + 14, Int16)
            Me.txtTransLimit.TabIndex = CType(TabIndexStartingNumber + 15, Int16)
            Me.cboBankSortCodes.TabIndex = CType(TabIndexStartingNumber + 16, Int16)
        End If
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Me.Page.BindBOPropertyToLabel(Me.State.myBankInfoBo, "Account_Name", labelNameonAccount)
        Me.Page.BindBOPropertyToLabel(Me.State.myBankInfoBo, "Bank_Id", labelBankID)
        Me.Page.BindBOPropertyToLabel(Me.State.myBankInfoBo, "Account_Number", labelBankAccountNo)
        Me.Page.BindBOPropertyToLabel(Me.State.myBankInfoBo, "CountryID", labelCountryOfBank)
        Me.Page.BindBOPropertyToLabel(Me.State.myBankInfoBo, "SwiftCode", labelSwiftCode)
        Me.Page.BindBOPropertyToLabel(Me.State.myBankInfoBo, "IbanNumber", labelIBAN_Number)
        Me.Page.BindBOPropertyToLabel(Me.State.myBankInfoBo, "AccountTypeId", labelAccountType)
        Me.Page.BindBOPropertyToLabel(Me.State.myBankInfoBo, "TransactionLimit", labelTranslimit)
        Me.Page.BindBOPropertyToLabel(Me.State.myBankInfoBo, "BankLookupCode", labelBanklookup)
        Me.Page.BindBOPropertyToLabel(Me.State.myBankInfoBo, "BankSubCode", labelbanksubcode)
        Me.Page.BindBOPropertyToLabel(Me.State.myBankInfoBo, "BankSortCode", labelBankSortCode)
        Me.Page.BindBOPropertyToLabel(Me.State.myBankInfoBo, "BranchDigit", lblBranchDigit)
        Me.Page.BindBOPropertyToLabel(Me.State.myBankInfoBo, "AccountDigit", lblAcctDigit)
        Me.Page.BindBOPropertyToLabel(Me.State.myBankInfoBo, "BranchNumber", lblBranchNumber)
        Me.Page.BindBOPropertyToLabel(Me.State.myBankInfoBo, "BankName", lblBankName)
        Me.Page.BindBOPropertyToLabel(Me.State.myBankInfoBo, "TaxId", lblTaxId)

        Me.Page.BindBOPropertyToLabel(Me.State.payeeAddress, "Address1", labelAddress1)
        Me.Page.BindBOPropertyToLabel(Me.State.payeeAddress, "Address2", labelAddress2)
        Me.Page.BindBOPropertyToLabel(Me.State.payeeAddress, "City", labelCity)
        Me.Page.BindBOPropertyToLabel(Me.State.payeeAddress, "PostalCode", labelPostalCode)

        ' Move to BankInfo BO 02/08/2008
        Me.Page.ClearGridViewHeadersAndLabelsErrorSign()
    End Sub

    Public Function PopulateAddressInfo() As Address
        Me.State.payeeAddress = New Address()
        Me.BindBoPropertiesToLabels()
        Me.Page.PopulateBOProperty(Me.State.payeeAddress, "Address1", txtAddress1)
        Me.Page.PopulateBOProperty(Me.State.payeeAddress, "Address2", txtAddress2)
        Me.Page.PopulateBOProperty(Me.State.payeeAddress, "City", txtCity)
        Me.Page.PopulateBOProperty(Me.State.payeeAddress, "PostalCode", txtPostalCode)
        Me.Page.PopulateBOProperty(Me.State.payeeAddress, "CountryId", Me.State.myBankInfoBo.CountryID)

        Return Me.State.payeeAddress
    End Function

    Public Sub PopulateBOFromControl(Optional ByVal blnExcludeSave As Boolean = False, Optional ByVal blnValidate As Boolean = True)
        Dim guidTemp As Guid, dTemp As DecimalType, LTemp As Long
        If Not Me.State.myBankInfoBo Is Nothing Then
            With Me.State.myBankInfoBo
                Me.BindBoPropertiesToLabels()

                If .IsNew AndAlso .Account_Name <> textboxNameAccount.Text Then State.IsNewObjDirty = True
                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo, "Account_Name", textboxNameAccount)

                If .IsNew AndAlso .Bank_Id <> textboxBankID.Text Then State.IsNewObjDirty = True
                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo, "Bank_Id", textboxBankID)

                If .IsNew AndAlso .Account_Number <> textboxBankAccountNo.Text Then State.IsNewObjDirty = True

                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo, "Account_Number", textboxBankAccountNo)

                guidTemp = .CountryID
                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo, "CountryID", moCountryDrop_WRITE)
                If .IsNew AndAlso .CountryID <> guidTemp Then State.IsNewObjDirty = True

                If .IsNew AndAlso .IbanNumber <> textboxIBAN_Number.Text Then State.IsNewObjDirty = True

                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo, "IbanNumber", textboxIBAN_Number)

                If .IsNew AndAlso .SwiftCode <> textboxSwiftCode.Text Then State.IsNewObjDirty = True
                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo, "SwiftCode", textboxSwiftCode)

                guidTemp = .AccountTypeId
                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo, "AccountTypeId", moAccountTypeDrop)
                If .IsNew AndAlso .AccountTypeId <> guidTemp Then State.IsNewObjDirty = True

                dTemp = .TransactionLimit
                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo, "TransactionLimit", txtTransLimit)
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
                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo, "BankLookupCode", txtBankLookupCode)

                If .IsNew AndAlso .BankSubCode <> txtBankSubcode.Text Then State.IsNewObjDirty = True
                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo, "BankSubCode", txtBankSubcode)

                If txtBankName.Visible = True Then
                    If .IsNew AndAlso .BankName <> txtBankName.Text Then State.IsNewObjDirty = True
                    Me.Page.PopulateBOProperty(Me.State.myBankInfoBo, "BankName", txtBankName)
                Else
                    'guidTemp = .AccountTypeId
                    Me.Page.PopulateBOProperty(Me.State.myBankInfoBo, "BankName", moBankName, False)
                    If .IsNew AndAlso (moBankName.SelectedIndex <> -1 AndAlso .BankName <> moBankName.SelectedItem.Text) Then State.IsNewObjDirty = True
                End If

                If .IsNew AndAlso .BranchName <> txtBankBranchName.Text Then State.IsNewObjDirty = True
                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo, "BranchName", txtBankBranchName)

                If .IsNew AndAlso .BankSortCode <> txtBankSortCode.Text Then State.IsNewObjDirty = True
                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo, "BankSortCode", txtBankSortCode)

                If cboBankSortCodes.Visible = True Then
                    Me.Page.PopulateBOProperty(Me.State.myBankInfoBo, "BankSortCode", cboBankSortCodes, False)
                    If .IsNew AndAlso cboBankSortCodes.SelectedIndex <> -1 Then State.IsNewObjDirty = True
                End If

                If .IsNew AndAlso .BranchDigit Is Nothing AndAlso .BranchDigit <> LongType.Parse(txtBranchDigit.Text) Then State.IsNewObjDirty = True
                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo, "BranchDigit", txtBranchDigit)

                If .IsNew AndAlso .AccountDigit Is Nothing AndAlso .AccountDigit <> LongType.Parse(txtAcctDigit.Text) Then State.IsNewObjDirty = True
                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo, "AccountDigit", txtAcctDigit)

                If .IsNew AndAlso .BranchNumber Is Nothing AndAlso .BranchNumber <> LongType.Parse(txtBranchNumber.Text) Then State.IsNewObjDirty = True
                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo, "BranchNumber", txtBranchNumber)

                If .IsNew AndAlso .TaxId <> txtTaxId.Text Then State.IsNewObjDirty = True
                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo, "TaxId", txtTaxId)

                If .IsNew AndAlso .BankInfoAddress.Address1 <> txtAddress1.Text Then State.IsNewObjDirty = True
                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo.BankInfoAddress, "Address1", txtAddress1)

                If .IsNew AndAlso .BankInfoAddress.Address2 <> txtAddress2.Text Then State.IsNewObjDirty = True
                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo.BankInfoAddress, "Address2", txtAddress2)

                If .IsNew AndAlso .BankInfoAddress.City <> txtCity.Text Then State.IsNewObjDirty = True
                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo.BankInfoAddress, "City", txtCity)

                If .IsNew AndAlso .BankInfoAddress.PostalCode <> txtPostalCode.Text Then State.IsNewObjDirty = True
                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo.BankInfoAddress, "PostalCode", txtPostalCode)

                'If txtAutoBankName.Text.Trim <> String.Empty AndAlso txtAutoBankName.Text.Trim.ToUpper <> inpDefaultBNDesc.Value.Trim.ToUpper Then
                '   ErrCollection.Add(New PopulateBOPropException(TranslationBase.TranslateLabelOrMessage("DEFAULT_SC_FOR_DENIED_CLAIMS"), txtAutoBankName, lblDefaultSCForDeniedClaims, New Exception("Service Center Not Found")))
                'Else               
                'End If
                ' Move to BankInfo BO 02/08/2008
                If blnValidate AndAlso .IsDirty Then
                    .Validate()
                    If Me.Page.ErrCollection.Count > 0 Then
                        Throw New PopulateBOErrorException
                    End If
                End If

                If Not blnExcludeSave Then .Save()
            End With
        End If
    End Sub

    Private Sub PopulateControlFromBo()
        If Not Me.State.myBankInfoBo Is Nothing Then

            PopulateAccountTypeDropdown()
            With Me.State.myBankInfoBo
                If Not .CountryID.Equals(Guid.Empty) Then
                    LoadCountryList(False)
                Else
                    LoadCountryList()
                End If

                If .ValidateFieldsforFR Then
                    Me.Page.PopulateControlFromBOProperty(Me.textboxNameAccount, .Account_Name)
                    Me.Page.PopulateControlFromBOProperty(Me.textboxBankAccountNo, .Account_Number)
                    Me.Page.PopulateControlFromBOProperty(Me.textboxIBAN_Number, .IbanNumber)
                Else
                    Me.Page.PopulateControlFromBOProperty(Me.textboxNameAccount, .Account_Name)
                    Me.Page.PopulateControlFromBOProperty(Me.textboxBankAccountNo, .Account_Number)
                    Me.Page.PopulateControlFromBOProperty(Me.textboxIBAN_Number, .IbanNumber)
                End If

                Me.Page.PopulateControlFromBOProperty(Me.textboxBankID, .Bank_Id)

                Me.Page.PopulateControlFromBOProperty(Me.textboxSwiftCode, .SwiftCode)
                Me.Page.SetSelectedItem(moCountryDrop_WRITE, .CountryID)
                If .AccountTypeId.Equals(System.Guid.Empty) Then
                    moAccountTypeDrop.SelectedIndex = 0
                Else
                    Me.Page.SetSelectedItem(moAccountTypeDrop, .AccountTypeId)
                End If

                If Me.txtBankName.Visible Then
                    Page.PopulateControlFromBOProperty(Me.txtBankName, .BankName)
                ElseIf moBankName.Visible Then
                    If Not .BankName Is Nothing Then Me.Page.SetSelectedItemByText(Me.moBankName, .BankName)
                Else
                    If Not .BankName Is Nothing Then
                        If (moBankName.Items.Count > 0) Then
                            Me.Page.SetSelectedItemByText(Me.moBankName, .BankName)
                        Else
                            Page.PopulateControlFromBOProperty(Me.txtBankName, .BankName)
                        End If
                    End If
                End If

                Page.PopulateControlFromBOProperty(Me.txtTransLimit, .TransactionLimit)
                Page.PopulateControlFromBOProperty(Me.txtBankLookupCode, .BankLookupCode)
                Page.PopulateControlFromBOProperty(Me.txtBankSubcode, .BankSubCode)
                'Page.PopulateControlFromBOProperty(Me.txtBankName, .BankName)
                Page.PopulateControlFromBOProperty(Me.txtBankBranchName, .BranchName)
                Page.PopulateControlFromBOProperty(Me.txtBankSortCode, .BankSortCode)

                If cboBankSortCodes.Visible Then
                    If Not .BankSortCode Is Nothing Then
                        Me.Page.SetSelectedItemByText(Me.cboBankSortCodes, .BankName)
                    ElseIf (cboBankSortCodes.Items.Count > 0) Then
                        Me.Page.SetSelectedItemByText(Me.cboBankSortCodes, .BankName)
                    End If
                End If

                Page.PopulateControlFromBOProperty(Me.txtBranchDigit, .BranchDigit)
                Page.PopulateControlFromBOProperty(Me.txtAcctDigit, .AccountDigit)
                Page.PopulateControlFromBOProperty(Me.txtBranchNumber, .BranchNumber)
                Page.PopulateControlFromBOProperty(Me.txtTaxId, .TaxId)

                Page.PopulateControlFromBOProperty(Me.txtAddress1, .BankInfoAddress.Address1)
                Page.PopulateControlFromBOProperty(Me.txtAddress2, .BankInfoAddress.Address2)
                Page.PopulateControlFromBOProperty(Me.txtCity, .BankInfoAddress.City)
                Page.PopulateControlFromBOProperty(Me.txtPostalCode, .BankInfoAddress.PostalCode)

            End With
        End If
    End Sub

    Public Sub SetCountryValue(ByVal oCountryID As Guid)
        Me.Page.SetSelectedItem(moCountryDrop_WRITE, oCountryID)
    End Sub

    Public Sub ChangeEnabledControlProperty(ByVal blnEnabledState As Boolean)
        Page.ChangeEnabledControlProperty(Me.textboxNameAccount, blnEnabledState)
        Page.ChangeEnabledControlProperty(Me.moCountryDrop_WRITE, blnEnabledState)
        Page.ChangeEnabledControlProperty(Me.textboxBankID, blnEnabledState)
        Page.ChangeEnabledControlProperty(Me.textboxBankAccountNo, blnEnabledState)
        Page.ChangeEnabledControlProperty(Me.textboxIBAN_Number, blnEnabledState)
        Page.ChangeEnabledControlProperty(Me.textboxSwiftCode, blnEnabledState)
        Page.ChangeEnabledControlProperty(Me.moAccountTypeDrop, blnEnabledState)
        Page.ChangeEnabledControlProperty(Me.txtBankLookupCode, blnEnabledState)
        Page.ChangeEnabledControlProperty(Me.txtBankSubcode, blnEnabledState)
        Page.ChangeEnabledControlProperty(Me.txtTransLimit, blnEnabledState)
        Page.ChangeEnabledControlProperty(Me.txtBankName, blnEnabledState)
        Page.ChangeEnabledControlProperty(Me.txtBankBranchName, blnEnabledState)
        Page.ChangeEnabledControlProperty(Me.txtBankSortCode, blnEnabledState)
        Page.ChangeEnabledControlProperty(Me.txtBranchDigit, blnEnabledState)
        Page.ChangeEnabledControlProperty(Me.txtAcctDigit, blnEnabledState)
        Page.ChangeEnabledControlProperty(Me.txtTaxId, blnEnabledState)
    End Sub

    Private Sub LoadCountryList(Optional ByVal nothingSelcted As Boolean = True)
        Dim oCountryList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")
        moCountryDrop_WRITE.Populate(oCountryList, New PopulateOptions() With
                                        {
                                        .AddBlankItem = nothingSelcted
                                        })


    End Sub
    Public Sub LoadBankSortCodeList(ByVal oCompany As Company, ByVal certTaxIdNumber As String)
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
        If Not oCompany.AttributeValues.Value(Codes.DEFAULT_CLAIM_BANK_SUB_CODE) Is Nothing Then
            txtBankSubcode.Text = oCompany.AttributeValues.Value(Codes.DEFAULT_CLAIM_BANK_SUB_CODE)
        End If

        If (Not String.IsNullOrEmpty(certTaxIdNumber) And Not oCompany.AttributeValues.Value(Codes.AUTO_POPULATE_CERT_TAX_ID) Is Nothing And oCompany.AttributeValues.Value(Codes.AUTO_POPULATE_CERT_TAX_ID) = Codes.YESNO_Y) Then
            txtTaxId.Text = certTaxIdNumber
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

    Public Sub SetRequiredFieldsForDealerWithGiftCard1(ByVal dealer As Dealer)
        If (dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_DARTY_GIFT_CARD_TYPE).Count > 0) Then
            If labelAddress1.Text.IndexOf("*") <> 0 Then Me.labelAddress1.Text = "* " & Me.labelAddress1.Text
            If labelCity.Text.IndexOf("*") <> 0 Then Me.labelCity.Text = "* " & Me.labelCity.Text
            If labelPostalCode.Text.IndexOf("*") <> 0 Then Me.labelPostalCode.Text = "* " & Me.labelPostalCode.Text
        End If
    End Sub

    Private Sub PopulateBankNameDropdown(ByVal SelectedCountryID As Guid)
        '    Dim obank As New BankName
        Dim BankDV As DataView = New DataView(BankName.LoadBankNameByCountry(SelectedCountryID))
        Page.BindListControlToDataView(moBankName, BankDV, "DESCRIPTION", "ID", True)

    End Sub

    Private Sub DisplayBankID()
        ControlMgr.SetVisibleControl(Me.Page, labelBankID, True)
        ControlMgr.SetVisibleControl(Me.Page, textboxBankID, True)
        ControlMgr.SetVisibleControl(Me.Page, LabelSpaceBankID, True)
    End Sub

    Private Sub HideNameonAccount()
        ControlMgr.SetVisibleControl(Me.Page, labelNameonAccount, False)
        ControlMgr.SetVisibleControl(Me.Page, textboxNameAccount, False)
        textboxNameAccount.Text = String.Empty
    End Sub

    Private Sub HideCountryOfBank()
        ControlMgr.SetVisibleControl(Me.Page, labelCountryOfBank, False)
        ControlMgr.SetVisibleControl(Me.Page, moCountryDrop_WRITE, False)
    End Sub

    Private Sub HideBankId()
        ControlMgr.SetVisibleControl(Me.Page, labelBankID, False)
        ControlMgr.SetVisibleControl(Me.Page, textboxBankID, False)
        ControlMgr.SetVisibleControl(Me.Page, LabelSpaceBankID, False)
        textboxBankID.Text = String.Empty
    End Sub

    Private Sub DisplayBankNameDP()
        ControlMgr.SetVisibleControl(Me.Page, lblBankName, True)
        ControlMgr.SetVisibleControl(Me.Page, moBankName, True)
        ControlMgr.SetVisibleControl(Me.Page, txtBankName, False)
        ControlMgr.SetVisibleControl(Me.Page, textboxBankID, True)
        textboxBankID.Text = String.Empty
    End Sub
    Private Sub HideBankName()
        ControlMgr.SetVisibleControl(Me.Page, moBankName, False)
        ControlMgr.SetVisibleControl(Me.Page, txtBankName, False)
        ControlMgr.SetVisibleControl(Me.Page, lblBankName, False)

    End Sub

    Private Sub DisplayBankNametxt()
        ControlMgr.SetVisibleControl(Me.Page, lblBankName, True)
        ControlMgr.SetVisibleControl(Me.Page, txtBankName, True)
        ControlMgr.SetVisibleControl(Me.Page, moBankName, False)
        moBankName.SelectedIndex = -1
    End Sub

    Private Sub DisplayBankAccNo()
        ControlMgr.SetVisibleControl(Me.Page, labelBankAccountNo, True)
        ControlMgr.SetVisibleControl(Me.Page, textboxBankAccountNo, True)
        ControlMgr.SetVisibleControl(Me.Page, LabelSpaceAccNo, True)
    End Sub

    Private Sub HideBankAccNo()
        ControlMgr.SetVisibleControl(Me.Page, labelBankAccountNo, False)
        ControlMgr.SetVisibleControl(Me.Page, labelBankAccountNo_Last4Digits, False)
        ControlMgr.SetVisibleControl(Me.Page, textboxBankAccountNo, False)
        ControlMgr.SetVisibleControl(Me.Page, LabelSpaceAccNo, False)
        textboxBankAccountNo.Text = String.Empty
    End Sub

    Private Sub DisplaySwiftCode()
        ControlMgr.SetVisibleControl(Me.Page, labelSwiftCode, True)
        ControlMgr.SetVisibleControl(Me.Page, textboxSwiftCode, True)
        ControlMgr.SetVisibleControl(Me.Page, LabelSpaceSwiftCode, True)
    End Sub

    Private Sub HideSwiftCode()
        ControlMgr.SetVisibleControl(Me.Page, labelSwiftCode, False)
        ControlMgr.SetVisibleControl(Me.Page, textboxSwiftCode, False)
        ControlMgr.SetVisibleControl(Me.Page, LabelSpaceSwiftCode, False)
        textboxSwiftCode.Text = String.Empty
    End Sub

    Private Sub DisplayIBAN_Number()
        ControlMgr.SetVisibleControl(Me.Page, labelIBAN_Number, True)
        ControlMgr.SetVisibleControl(Me.Page, textboxIBAN_Number, True)
        ControlMgr.SetVisibleControl(Me.Page, LabelSpaceIBAN, True)
    End Sub

    Private Sub HideIBAN_Number()
        ControlMgr.SetVisibleControl(Me.Page, labelIBAN_Number, False)
        ControlMgr.SetVisibleControl(Me.Page, textboxIBAN_Number, False)
        ControlMgr.SetVisibleControl(Me.Page, LabelSpaceIBAN, False)
        textboxIBAN_Number.Text = String.Empty
    End Sub
    Private Sub DisplayAccountDigit()
        ControlMgr.SetVisibleControl(Me.Page, lblAcctDigit, True)
        ControlMgr.SetVisibleControl(Me.Page, txtAcctDigit, True)
        ControlMgr.SetVisibleControl(Me.Page, lblBankAcctDigit, True)
    End Sub

    Private Sub HideAccountDigit()
        ControlMgr.SetVisibleControl(Me.Page, lblAcctDigit, False)
        ControlMgr.SetVisibleControl(Me.Page, txtAcctDigit, False)
        ControlMgr.SetVisibleControl(Me.Page, lblBankAcctDigit, False)
        txtAcctDigit.Text = String.Empty
    End Sub

    Private Sub DisplayBranchDigit()
        ControlMgr.SetVisibleControl(Me.Page, lblBranchDigit, True)
        ControlMgr.SetVisibleControl(Me.Page, txtBranchDigit, True)
        ControlMgr.SetVisibleControl(Me.Page, lblBankBranchDigit, True)
    End Sub

    Private Sub HideBranchDigit()
        ControlMgr.SetVisibleControl(Me.Page, lblBranchDigit, False)
        ControlMgr.SetVisibleControl(Me.Page, txtBranchDigit, False)
        ControlMgr.SetVisibleControl(Me.Page, lblBankBranchDigit, False)
        txtBranchDigit.Text = String.Empty
    End Sub

    Private Sub HideTranslimit()
        ControlMgr.SetVisibleControl(Me.Page, labelTranslimit, False)
        ControlMgr.SetVisibleControl(Me.Page, txtTransLimit, False)
        ControlMgr.SetVisibleControl(Me.Page, lblTransLimit, False)
        txtTransLimit.Text = String.Empty
    End Sub
    Private Sub DisplayTranslimit()
        ControlMgr.SetVisibleControl(Me.Page, labelTranslimit, True)
        ControlMgr.SetVisibleControl(Me.Page, txtTransLimit, True)
        ControlMgr.SetVisibleControl(Me.Page, lblTransLimit, True)
    End Sub
    Private Sub HideBankSubCode()
        ControlMgr.SetVisibleControl(Me.Page, labelbanksubcode, False)
        ControlMgr.SetVisibleControl(Me.Page, txtBankSubcode, False)
        ControlMgr.SetVisibleControl(Me.Page, lblBankSubcode, False)
        txtBankSubcode.Text = String.Empty
    End Sub
    Private Sub DisplayBankSubCode()
        ControlMgr.SetVisibleControl(Me.Page, lblBankSubcode, True)
        ControlMgr.SetVisibleControl(Me.Page, txtBankSubcode, True)
        ControlMgr.SetVisibleControl(Me.Page, labelbanksubcode, True)
    End Sub

    Private Sub HideBanklookupCode()
        ControlMgr.SetVisibleControl(Me.Page, labelBanklookup, False)
        ControlMgr.SetVisibleControl(Me.Page, txtBankLookupCode, False)
        ControlMgr.SetVisibleControl(Me.Page, lblBankLookupCode, False)
        txtBankLookupCode.Text = String.Empty
    End Sub
    Private Sub DisplayBankLookupCode()
        ControlMgr.SetVisibleControl(Me.Page, labelBanklookup, True)
        ControlMgr.SetVisibleControl(Me.Page, txtBankLookupCode, True)
        ControlMgr.SetVisibleControl(Me.Page, lblBankLookupCode, True)
    End Sub
    Private Sub HideBankSortCode()
        ControlMgr.SetVisibleControl(Me.Page, labelBankSortCode, False)
        ControlMgr.SetVisibleControl(Me.Page, txtBankSortCode, False)
        ControlMgr.SetVisibleControl(Me.Page, lblBankSortCode, False)
        txtBankSubcode.Text = String.Empty
    End Sub
    Private Sub DisplayBankSortCode()
        ControlMgr.SetVisibleControl(Me.Page, labelBankSortCode, True)
        ControlMgr.SetVisibleControl(Me.Page, txtBankSortCode, True)
        ControlMgr.SetVisibleControl(Me.Page, lblBankSortCode, True)
        ControlMgr.SetVisibleControl(Me.Page, cboBankSortCodes, False)
    End Sub
    Public Sub HideCboBankSortCode()
        ControlMgr.SetVisibleControl(Me.Page, labelBankSortCode, True)
        ControlMgr.SetVisibleControl(Me.Page, txtBankSortCode, True)
        ControlMgr.SetVisibleControl(Me.Page, lblBankSortCode, True)
        ControlMgr.SetVisibleControl(Me.Page, cboBankSortCodes, False)
    End Sub
    Private Sub DisplayCboBankSortCode()
        ControlMgr.SetVisibleControl(Me.Page, labelBankSortCode, True)
        ControlMgr.SetVisibleControl(Me.Page, cboBankSortCodes, True)
        ControlMgr.SetVisibleControl(Me.Page, txtBankSortCode, False)
        ControlMgr.SetVisibleControl(Me.Page, lblBankSortCode, False)
    End Sub
    Private Sub HideBranchName()
        ControlMgr.SetVisibleControl(Me.Page, lblBranchName, False)
        ControlMgr.SetVisibleControl(Me.Page, txtBankBranchName, False)
        ControlMgr.SetVisibleControl(Me.Page, lblSpaceBranchName, False)
        txtBankBranchName.Text = String.Empty
    End Sub
    Private Sub DisplayBranchName()
        ControlMgr.SetVisibleControl(Me.Page, lblBranchName, True)
        ControlMgr.SetVisibleControl(Me.Page, txtBankBranchName, True)
        ControlMgr.SetVisibleControl(Me.Page, lblSpaceBranchName, True)
    End Sub

    Private Sub HideBranchNumber()
        ControlMgr.SetVisibleControl(Me.Page, lblBranchNumber, False)
        ControlMgr.SetVisibleControl(Me.Page, txtBranchNumber, False)
        ControlMgr.SetVisibleControl(Me.Page, lblBankBranchNumber, False)
        txtBranchNumber.Text = String.Empty
    End Sub
    Private Sub DisplayBranchNumber()
        ControlMgr.SetVisibleControl(Me.Page, lblBranchNumber, True)
        ControlMgr.SetVisibleControl(Me.Page, txtBranchNumber, True)
        ControlMgr.SetVisibleControl(Me.Page, lblBankBranchNumber, True)
    End Sub
    Public Sub HideTaxId()
        ControlMgr.SetVisibleControl(Me.Page, labelTaxId, False)
        ControlMgr.SetVisibleControl(Me.Page, txtTaxId, False)
        ControlMgr.SetVisibleControl(Me.Page, lblTaxId, False)
        txtTaxId.Text = String.Empty
    End Sub
    Public Sub DisplayTaxId()
        ControlMgr.SetVisibleControl(Me.Page, labelTaxId, True)
        ControlMgr.SetVisibleControl(Me.Page, txtTaxId, True)
        ControlMgr.SetVisibleControl(Me.Page, lblTaxId, True)
    End Sub

    Private Sub DisplayAccountType()
        ControlMgr.SetVisibleControl(Me.Page, labelAccountType, True)
        ControlMgr.SetVisibleControl(Me.Page, moAccountTypeDrop, True)
    End Sub
    Private Sub HideAccountType()
        ControlMgr.SetVisibleControl(Me.Page, labelAccountType, False)
        ControlMgr.SetVisibleControl(Me.Page, moAccountTypeDrop, False)
    End Sub

    Private Sub DisplayAddressControls()
        ControlMgr.SetVisibleControl(Me.Page, labelAddress1, True)
        ControlMgr.SetVisibleControl(Me.Page, lblAddress1, True)
        ControlMgr.SetVisibleControl(Me.Page, txtAddress1, True)

        ControlMgr.SetVisibleControl(Me.Page, labelAddress2, True)
        ControlMgr.SetVisibleControl(Me.Page, lblAddress2, True)
        ControlMgr.SetVisibleControl(Me.Page, txtAddress2, True)

        ControlMgr.SetVisibleControl(Me.Page, labelCity, True)
        ControlMgr.SetVisibleControl(Me.Page, lblCity, True)
        ControlMgr.SetVisibleControl(Me.Page, txtCity, True)

        ControlMgr.SetVisibleControl(Me.Page, labelPostalCode, True)
        ControlMgr.SetVisibleControl(Me.Page, lblPostalCode, True)
        ControlMgr.SetVisibleControl(Me.Page, txtPostalCode, True)
    End Sub

    Private Sub HideAddressControls()
        ControlMgr.SetVisibleControl(Me.Page, labelAddress1, False)
        ControlMgr.SetVisibleControl(Me.Page, lblAddress1, False)
        ControlMgr.SetVisibleControl(Me.Page, txtAddress1, False)

        ControlMgr.SetVisibleControl(Me.Page, labelAddress2, False)
        ControlMgr.SetVisibleControl(Me.Page, lblAddress2, False)
        ControlMgr.SetVisibleControl(Me.Page, txtAddress2, False)

        ControlMgr.SetVisibleControl(Me.Page, labelCity, False)
        ControlMgr.SetVisibleControl(Me.Page, lblCity, False)
        ControlMgr.SetVisibleControl(Me.Page, txtCity, False)

        ControlMgr.SetVisibleControl(Me.Page, labelPostalCode, False)
        ControlMgr.SetVisibleControl(Me.Page, lblPostalCode, False)
        ControlMgr.SetVisibleControl(Me.Page, txtPostalCode, False)
    End Sub

    Private Sub DomesticTransfer()
        If Not Me.State.myBankInfoBo Is Nothing Then
            Me.State.myBankInfoBo.DomesticTransfer = True
            Me.State.myBankInfoBo.InternationalEUTransfer = False
            Me.State.myBankInfoBo.InternationalTransfer = False
        End If
        EnableDisableRequiredControls()
    End Sub
    Private Sub InternationalEUTransfer()
        If Not Me.State.myBankInfoBo Is Nothing Then
            Me.State.myBankInfoBo.DomesticTransfer = False
            Me.State.myBankInfoBo.InternationalEUTransfer = True
            Me.State.myBankInfoBo.InternationalTransfer = False
        End If
        EnableDisableRequiredControls()
    End Sub

    Private Sub InternationalTransfer()
        If Not Me.State.myBankInfoBo Is Nothing Then
            Me.State.myBankInfoBo.DomesticTransfer = False
            Me.State.myBankInfoBo.InternationalEUTransfer = False
            Me.State.myBankInfoBo.InternationalTransfer = True
        End If
        EnableDisableRequiredControls()
    End Sub

    Private Sub moCountryDrop_WRITE_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moCountryDrop_WRITE.SelectedIndexChanged
        If (Not (Me.State.myBankInfoBo.SourceCountryID.Equals(Guid.Empty))) And (Not (Me.Page.GetSelectedItem(Me.moCountryDrop_WRITE).Equals(Guid.Empty))) Then
            If Me.State.myBankInfoBo.SourceCountryID.Equals(Me.Page.GetSelectedItem(Me.moCountryDrop_WRITE)) Then
                'Domestic transfer
                DomesticTransfer()
            Else
                Dim objCountry As New Country(Me.Page.GetSelectedItem(Me.moCountryDrop_WRITE))
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objCountry.EuropeanCountryId) = Codes.YESNO_Y Then
                    'International transfer 
                    InternationalEUTransfer()
                Else
                    'International transfer & Destination is not a European country
                    InternationalTransfer()
                End If
            End If

            ' aDefaultBN.ContextKey = GuidControl.GuidToHexString(Me.Page.GetSelectedItem(Me.moCountryDrop_WRITE))
            EnableControlsBasedOnCountry(Me.Page.GetSelectedItem(Me.moCountryDrop_WRITE))
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

        If Not Me.State.myBankInfoBo Is Nothing Then

            If Me.State.myBankInfoBo.ValidateFieldsforFR = True Then

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

            ElseIf Me.State.myBankInfoBo.SepaEUBankTransfer = True Then
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

            ElseIf Me.State.myBankInfoBo.DomesticTransfer = True Then

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

            ElseIf Me.State.myBankInfoBo.InternationalTransfer = True Then

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


            ElseIf Me.State.myBankInfoBo.InternationalEUTransfer = True Then

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

    Public Sub GetBankIdForBankName(ByVal sender As Object, ByVal e As System.EventArgs) Handles moBankName.SelectedIndexChanged
        If Not Me.Page.GetSelectedItem(moBankName).Equals(Guid.Empty) Then
            Dim boBankName As BankName
            boBankName = New BankName(Me.Page.GetSelectedItem(moBankName))
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

    Public Sub EnableControlsBasedOnCountry(ByVal SelectedCountryId As Guid)

        'If Not (Me.Page.GetSelectedItem(Me.moCountryDrop_WRITE).Equals(Guid.Empty)) Then ' req-5383

        If Not (SelectedCountryId.Equals(Guid.Empty)) Then ' req-5383
            Dim ocountry As New Country(SelectedCountryId)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, ocountry.UseBankListId) = Codes.YESNO_Y Then
                DisplayBankNameDP()
                DisplayBankID()
                PopulateBankNameDropdown(SelectedCountryId)
                ControlMgr.SetEnableControl(Me.Page, textboxBankID, False)
            Else
                DisplayBankNametxt()
                ControlMgr.SetEnableControl(Me.Page, textboxBankID, True)
            End If
            If LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, ocountry.ValidateBankInfoId) = Codes.Country_Code_Brasil Then
                Me._validateBankInfoCountry = Codes.Country_Code_Brasil
                Me.State.myBankInfoBo.ValidateFieldsforBR = True

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
                Me._validateBankInfoCountry = Codes.Country_Code_Argentina
                Me.State.myBankInfoBo.ValidateFieldsforBR = False
                DisplayAccountType()
                DisplayAddressControls()

                HideAccountDigit()
                HideBranchDigit()
                HideBranchNumber()
                HideAddressControls()
            ElseIf (LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, ocountry.ValidateBankInfoId) = Codes.Country_Code_France) Then
                Dim Payee As String
                If (Not CType(Parent.FindControl("cboPayeeSelector"), DropDownList) Is Nothing) Then

                    If (CType(Parent.FindControl("cboPayeeSelector"), DropDownList).Items.Count > 0 And Not CType(Parent.FindControl("cboPayeeSelector"), DropDownList).SelectedItem Is Nothing) Then
                        Payee = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE, ElitaPlusPage.GetSelectedItem(CType(Parent.FindControl("cboPayeeSelector"), DropDownList)))
                    End If

                    If (Payee = ClaimInvoice.PAYEE_OPTION_CUSTOMER) Then
                        Me._validateBankInfoCountry = Codes.Country_Code_France

                        Me.State.myBankInfoBo.ValidateFieldsforBR = False
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
                Me.State.myBankInfoBo.ValidateFieldsforBR = False
                HideAccountDigit()
                HideBranchDigit()
                HideBranchNumber()
                HideAddressControls()
            End If
        End If

    End Sub

    Public Sub DisableAllFields()
        ControlMgr.SetEnableControl(Me.Page, textboxNameAccount, False)
        ControlMgr.SetEnableControl(Me.Page, moCountryDrop_WRITE, False)
        ControlMgr.SetEnableControl(Me.Page, txtBankName, False)
        ControlMgr.SetEnableControl(Me.Page, moBankName, False)
        ControlMgr.SetEnableControl(Me.Page, txtBankBranchName, False)
        ControlMgr.SetEnableControl(Me.Page, textboxBankID, False)
        ControlMgr.SetEnableControl(Me.Page, textboxBankAccountNo, False)
        ControlMgr.SetEnableControl(Me.Page, txtAcctDigit, False)
        ControlMgr.SetEnableControl(Me.Page, moAccountTypeDrop, False)
        ControlMgr.SetEnableControl(Me.Page, txtBranchNumber, False)
        ControlMgr.SetEnableControl(Me.Page, txtBranchDigit, False)
        ControlMgr.SetEnableControl(Me.Page, textboxSwiftCode, False)
        ControlMgr.SetEnableControl(Me.Page, textboxIBAN_Number, False)
        ControlMgr.SetEnableControl(Me.Page, txtBankLookupCode, False)
        ControlMgr.SetEnableControl(Me.Page, txtBankSubcode, False)
        ControlMgr.SetEnableControl(Me.Page, txtBankSortCode, False)
        ControlMgr.SetEnableControl(Me.Page, txtTransLimit, False)
        ControlMgr.SetEnableControl(Me.Page, txtTaxId, False)

        ControlMgr.SetEnableControl(Me.Page, txtAddress1, False)
        ControlMgr.SetEnableControl(Me.Page, txtAddress2, False)
        ControlMgr.SetEnableControl(Me.Page, txtCity, False)
        ControlMgr.SetEnableControl(Me.Page, txtPostalCode, False)

        'SwitchToLastFourDigitsLabelMode(Me.State.myBankInfoBo.ValidateFieldsforFR)

    End Sub



    Public Sub SetRequiredFieldsForDealerWithGiftCard(ByVal dealer As Dealer)
        If (dealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_DARTY_GIFT_CARD_TYPE).Count > 0) Then
            If labelAddress1.Text.IndexOf("*") <> 0 Then Me.labelAddress1.Text = "* " & Me.labelAddress1.Text
            If labelCity.Text.IndexOf("*") <> 0 Then Me.labelCity.Text = "* " & Me.labelCity.Text
            If labelPostalCode.Text.IndexOf("*") <> 0 Then Me.labelPostalCode.Text = "* " & Me.labelPostalCode.Text

        End If
    End Sub

    Private Sub SwitchToLastFourDigitsLabelMode(value As Boolean)
        labelIBAN_Number_Last4Digits.Visible = value
        labelIBAN_Number.Visible = Not value

        labelBankAccountNo_Last4Digits.Visible = value
        labelBankAccountNo.Visible = Not value
    End Sub

    Public Sub SwitchToEditView()
        If Me.State.myBankInfoBo.ValidateFieldsforFR Then
            ControlMgr.SetEnableControl(Me.Page, textboxIBAN_Number, True)
            ControlMgr.SetEnableControl(Me.Page, txtBankLookupCode, True)

            'textboxIBAN_Number.Text = String.Empty
            'txtBankLookupCode.Text = String.Empty

            'labelIBAN_Number_Last4Digits.Visible = False
            ' labelIBAN_Number.Visible = True
        End If
    End Sub
    Public Sub SwitchToReadOnlyView()
        If Me.State.myBankInfoBo.ValidateFieldsforFR Then
            ControlMgr.SetEnableControl(Me.Page, textboxIBAN_Number, False)
            ControlMgr.SetEnableControl(Me.Page, txtBankLookupCode, False)
        End If
    End Sub
End Class

