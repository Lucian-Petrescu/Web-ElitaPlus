Imports System.Diagnostics

Partial Class UserControlBankInfo
    Inherits UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As Object

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "State"
    Class MyState
        Public myBankInfoBo As BankInfo
        Public ErrControllerId As String
        Public IsNewObjDirty As Boolean = False
        Public Function IsBODirty() As Boolean
            If myBankInfoBo Is Nothing Then
                Return False
            Else
                If myBankInfoBo.IsNew Then
                    Return IsNewObjDirty
                Else
                    Return myBankInfoBo.IsDirty
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

    Private ReadOnly Property ErrCtrl() As ErrorController
        Get
            If State.ErrControllerId IsNot Nothing Then
                Return CType(Page.FindControl(State.ErrControllerId), ErrorController)
            End If
        End Get
    End Property
    Private Shadows ReadOnly Property Page() As ElitaPlusPage
        Get
            Return CType(MyBase.Page, ElitaPlusPage)
        End Get
    End Property

#End Region

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If State.myBankInfoBo IsNot Nothing Then
            BindBoPropertiesToLabels()
            If (Not (State.myBankInfoBo.SourceCountryID.Equals(Guid.Empty))) AndAlso (Not (Page.GetSelectedItem(moCountryDrop_WRITE).Equals(Guid.Empty))) Then
                If State.myBankInfoBo.SourceCountryID.Equals(Page.GetSelectedItem(moCountryDrop_WRITE)) Then
                    'Domestic transfer
                    DomesticTransfer()
                Else
                    Dim objCountry As New Country(Page.GetSelectedItem(moCountryDrop_WRITE))
                    If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objCountry.EuropeanCountryId) = Codes.YESNO_Y Then
                        'International transfer & Destination is European country
                        InternationalEUTransfer()
                    Else
                        'International transfer & Destination is not a European country
                        InternationalTransfer()
                    End If
                End If
            Else
                'Domestic transfer
                DomesticTransfer()
            End If
        Else
            SetTheRequiredFields()

            'Domestic transfer
            DomesticTransfer()
        End If
    End Sub


    Public Sub SetTheRequiredFields()
        If labelNameonAccount.Text.IndexOf("*") <> 0 Then labelNameonAccount.Text = "* " & labelNameonAccount.Text
        If labelBankID.Text.IndexOf("*") <> 0 Then labelBankID.Text = "* " & labelBankID.Text
        If labelBankAccountNo.Text.IndexOf("*") <> 0 Then labelBankAccountNo.Text = "* " & labelBankAccountNo.Text
        If labelCountryOfBank.Text.IndexOf("*") <> 0 Then labelCountryOfBank.Text = "* " & labelCountryOfBank.Text
        If labelSwiftCode.Text.IndexOf("*") <> 0 Then labelSwiftCode.Text = "* " & labelSwiftCode.Text
        If labelIBAN_Number.Text.IndexOf("*") <> 0 Then labelIBAN_Number.Text = "* " & labelIBAN_Number.Text
    End Sub
    Public Sub Bind(bankinfoBo As BankInfo, containerErrorController As ErrorController)
        With State
            .ErrControllerId = containerErrorController.ID
            .myBankInfoBo = bankinfoBo
        End With
        textboxBankID.Text = String.Empty
        textboxBankAccountNo.Text = String.Empty
        PopulateControlFromBo()

        If (Not (State.myBankInfoBo.SourceCountryID.Equals(Guid.Empty))) AndAlso (Not (Page.GetSelectedItem(moCountryDrop_WRITE).Equals(Guid.Empty))) Then
            If State.myBankInfoBo.SourceCountryID.Equals(Page.GetSelectedItem(moCountryDrop_WRITE)) Then
                'Domestic transfer
                DomesticTransfer()
            Else
                Dim objCountry As New Country(Page.GetSelectedItem(moCountryDrop_WRITE))
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objCountry.EuropeanCountryId) = Codes.YESNO_Y Then
                    'International transfer & Destination is European country
                    InternationalEUTransfer()
                Else
                    'International transfer & Destination is not a European country
                    InternationalTransfer()
                End If
            End If
        Else
            'Domestic transfer
            DomesticTransfer()
        End If

    End Sub
    Public Sub ReAssignTabIndex(Optional ByVal TabIndexStartingNumber As Int16 = 0)
        If TabIndexStartingNumber > 0 Then
            textboxNameAccount.TabIndex = TabIndexStartingNumber
            moCountryDrop_WRITE.TabIndex = CType(TabIndexStartingNumber + 1, Int16)
            textboxBankID.TabIndex = CType(TabIndexStartingNumber + 2, Int16)
            textboxBankAccountNo.TabIndex = CType(TabIndexStartingNumber + 3, Int16)
            txtBankName.TabIndex = CType(TabIndexStartingNumber + 4, Int16)
            txtBankBranchName.TabIndex = CType(TabIndexStartingNumber + 5, Int16)
            textboxSwiftCode.TabIndex = CType(TabIndexStartingNumber + 6, Int16)
            textboxIBAN_Number.TabIndex = CType(TabIndexStartingNumber + 7, Int16)
            moAccountTypeDrop.TabIndex = CType(TabIndexStartingNumber + 8, Int16)
            txtTransLimit.TabIndex = CType(TabIndexStartingNumber + 9, Int16)
            txtBankLookupCode.TabIndex = CType(TabIndexStartingNumber + 10, Int16)
            txtBankSubcode.TabIndex = CType(TabIndexStartingNumber + 11, Int16)
            txtBankSortCode.TabIndex = CType(TabIndexStartingNumber + 12, Int16)
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
        ' Move to BankInfo BO 02/08/2008
        Page.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Public Sub PopulateBOFromControl(Optional ByVal blnExcludeSave As Boolean = False, Optional ByVal blnValidate As Boolean = True)
        Dim guidTemp As Guid, dTemp As DecimalType
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

                If .IsNew AndAlso .BankName <> txtBankName.Text Then State.IsNewObjDirty = True
                Page.PopulateBOProperty(State.myBankInfoBo, "BankName", txtBankName)

                If .IsNew AndAlso .BranchName <> txtBankBranchName.Text Then State.IsNewObjDirty = True
                Page.PopulateBOProperty(State.myBankInfoBo, "BranchName", txtBankBranchName)

                If .IsNew AndAlso .BankSortCode <> txtBankSortCode.Text Then State.IsNewObjDirty = True
                Page.PopulateBOProperty(State.myBankInfoBo, "BankSortCode", txtBankSortCode)

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
                Page.PopulateControlFromBOProperty(textboxNameAccount, .Account_Name)
                Page.PopulateControlFromBOProperty(textboxBankAccountNo, .Account_Number)
                Page.PopulateControlFromBOProperty(textboxBankID, .Bank_Id)
                Page.PopulateControlFromBOProperty(textboxIBAN_Number, .IbanNumber)
                Page.PopulateControlFromBOProperty(textboxSwiftCode, .SwiftCode)
                Page.SetSelectedItem(moCountryDrop_WRITE, .CountryID)
                If .AccountTypeId.Equals(Guid.Empty) Then
                    moAccountTypeDrop.SelectedIndex = 0
                Else
                    Page.SetSelectedItem(moAccountTypeDrop, .AccountTypeId)
                End If
                Page.PopulateControlFromBOProperty(txtTransLimit, .TransactionLimit)
                Page.PopulateControlFromBOProperty(txtBankLookupCode, .BankLookupCode)
                Page.PopulateControlFromBOProperty(txtBankSubcode, .BankSubCode)
                Page.PopulateControlFromBOProperty(txtBankName, .BankName)
                Page.PopulateControlFromBOProperty(txtBankBranchName, .BranchName)
                Page.PopulateControlFromBOProperty(txtBankSortCode, .BankSortCode)
            End With
        End If
    End Sub
    Public Sub SetCountryValue(oCountryID As Guid)
        Page.SetSelectedItem(moCountryDrop_WRITE, oCountryID)
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
    End Sub
    Private Sub LoadCountryList(Optional ByVal nothingSelcted As Boolean = True)
        Dim oCountryList As DataView = LookupListNew.GetCountryLookupList()
        Page.BindListControlToDataView(moCountryDrop_WRITE, oCountryList, , , nothingSelcted)
    End Sub

    Private Sub PopulateAccountTypeDropdown()
        Dim AcctypeDV As DataView = LookupListNew.GetAccountTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        Page.BindListControlToDataView(moAccountTypeDrop, AcctypeDV, "DESCRIPTION", "ID", True)
    End Sub

    Private Sub DisplayBankID()
        ControlMgr.SetVisibleControl(Page, labelBankID, True)
        ControlMgr.SetVisibleControl(Page, textboxBankID, True)
        ControlMgr.SetVisibleControl(Page, LabelSpaceBankID, True)
    End Sub

    Private Sub HideBankId()
        ControlMgr.SetVisibleControl(Page, labelBankID, False)
        ControlMgr.SetVisibleControl(Page, textboxBankID, False)
        ControlMgr.SetVisibleControl(Page, LabelSpaceBankID, False)
        textboxBankID.Text = String.Empty
    End Sub

    Private Sub DisplayBankAccNo()
        ControlMgr.SetVisibleControl(Page, labelBankAccountNo, True)
        ControlMgr.SetVisibleControl(Page, textboxBankAccountNo, True)
        ControlMgr.SetVisibleControl(Page, LabelSpaceAccNo, True)
    End Sub

    Private Sub HideBankAccNo()
        ControlMgr.SetVisibleControl(Page, labelBankAccountNo, False)
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
    Private Sub DomesticTransfer()
        'Domestic transfer---display bank Id and bank Account # controls 
        DisplayBankID()
        DisplayBankAccNo()

        'Domestic transfer---hide swift code & Iban number controls
        HideSwiftCode()
        HideIBAN_Number()
        If State.myBankInfoBo IsNot Nothing Then
            State.myBankInfoBo.DomesticTransfer = True
            State.myBankInfoBo.InternationalEUTransfer = False
            State.myBankInfoBo.InternationalTransfer = False
        End If
    End Sub
    Private Sub InternationalEUTransfer()
        'International transfer & Destination is European country---hide bank Id and bank Account # controls 
        HideBankId()
        HideBankAccNo()

        'International transfer & Destination is European country---display swift code & Iban number controls
        DisplaySwiftCode()
        DisplayIBAN_Number()
        If State.myBankInfoBo IsNot Nothing Then
            State.myBankInfoBo.DomesticTransfer = False
            State.myBankInfoBo.InternationalEUTransfer = True
            State.myBankInfoBo.InternationalTransfer = False
        End If
    End Sub

    Private Sub InternationalTransfer()
        'International transfer & Destination is not a European country---display bank Id, bank Account #, Swift Code controls 
        DisplayBankID()
        DisplayBankAccNo()
        DisplaySwiftCode()

        'International transfer & Destination is European country---hide Iban number controls
        HideIBAN_Number()
        If State.myBankInfoBo IsNot Nothing Then
            State.myBankInfoBo.DomesticTransfer = False
            State.myBankInfoBo.InternationalEUTransfer = False
            State.myBankInfoBo.InternationalTransfer = True
        End If
    End Sub

    Private Sub moCountryDrop_WRITE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moCountryDrop_WRITE.SelectedIndexChanged
        If (Not (State.myBankInfoBo.SourceCountryID.Equals(Guid.Empty))) AndAlso (Not (Page.GetSelectedItem(moCountryDrop_WRITE).Equals(Guid.Empty))) Then
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
        Else
            'Domestic transfer
            DomesticTransfer()
        End If
    End Sub

    Public Sub EnableDisableControls()
        'when control is loaded for first time...assume it is a Domestic transfer
        'Domestic transfer---display bank Id and bank Account # controls 
        DisplayBankID()
        DisplayBankAccNo()

        'Domestic transfer---hide swift code & Iban number controls
        HideSwiftCode()
        HideIBAN_Number()
    End Sub
End Class
