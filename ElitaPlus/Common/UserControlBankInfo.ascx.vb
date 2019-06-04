Partial Class UserControlBankInfo
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
            If Me.Page.StateSession.Item(Me.UniqueID) Is Nothing Then
                Me.Page.StateSession.Item(Me.UniqueID) = New MyState
            End If
            Return CType(Me.Page.StateSession.Item(Me.UniqueID), MyState)
        End Get
    End Property

    Private ReadOnly Property ErrCtrl() As ErrorController
        Get
            If Not Me.State.ErrControllerId Is Nothing Then
                Return CType(Me.Page.FindControl(Me.State.ErrControllerId), ErrorController)
            End If
        End Get
    End Property
    Private Shadows ReadOnly Property Page() As ElitaPlusPage
        Get
            Return CType(MyBase.Page, ElitaPlusPage)
        End Get
    End Property

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not Me.State.myBankInfoBo Is Nothing Then
            BindBoPropertiesToLabels()
            If (Not (Me.State.myBankInfoBo.SourceCountryID.Equals(Guid.Empty))) And (Not (Me.Page.GetSelectedItem(Me.moCountryDrop_WRITE).Equals(Guid.Empty))) Then
                If Me.State.myBankInfoBo.SourceCountryID.Equals(Me.Page.GetSelectedItem(Me.moCountryDrop_WRITE)) Then
                    'Domestic transfer
                    DomesticTransfer()
                Else
                    Dim objCountry As New Country(Me.Page.GetSelectedItem(Me.moCountryDrop_WRITE))
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
        If labelNameonAccount.Text.IndexOf("*") <> 0 Then Me.labelNameonAccount.Text = "* " & Me.labelNameonAccount.Text
        If labelBankID.Text.IndexOf("*") <> 0 Then Me.labelBankID.Text = "* " & Me.labelBankID.Text
        If labelBankAccountNo.Text.IndexOf("*") <> 0 Then Me.labelBankAccountNo.Text = "* " & Me.labelBankAccountNo.Text
        If labelCountryOfBank.Text.IndexOf("*") <> 0 Then Me.labelCountryOfBank.Text = "* " & Me.labelCountryOfBank.Text
        If labelSwiftCode.Text.IndexOf("*") <> 0 Then Me.labelSwiftCode.Text = "* " & Me.labelSwiftCode.Text
        If labelIBAN_Number.Text.IndexOf("*") <> 0 Then Me.labelIBAN_Number.Text = "* " & Me.labelIBAN_Number.Text
    End Sub
    Public Sub Bind(ByVal bankinfoBo As BankInfo, ByVal containerErrorController As ErrorController)
        With State
            .ErrControllerId = containerErrorController.ID
            .myBankInfoBo = bankinfoBo
        End With
        Me.textboxBankID.Text = String.Empty
        Me.textboxBankAccountNo.Text = String.Empty
        Me.PopulateControlFromBo()

        If (Not (Me.State.myBankInfoBo.SourceCountryID.Equals(Guid.Empty))) AndAlso (Not (Me.Page.GetSelectedItem(Me.moCountryDrop_WRITE).Equals(Guid.Empty))) Then
            If Me.State.myBankInfoBo.SourceCountryID.Equals(Me.Page.GetSelectedItem(Me.moCountryDrop_WRITE)) Then
                'Domestic transfer
                DomesticTransfer()
            Else
                Dim objCountry As New Country(Me.Page.GetSelectedItem(Me.moCountryDrop_WRITE))
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
            Me.textboxNameAccount.TabIndex = TabIndexStartingNumber
            Me.moCountryDrop_WRITE.TabIndex = CType(TabIndexStartingNumber + 1, Int16)
            Me.textboxBankID.TabIndex = CType(TabIndexStartingNumber + 2, Int16)
            Me.textboxBankAccountNo.TabIndex = CType(TabIndexStartingNumber + 3, Int16)
            Me.txtBankName.TabIndex = CType(TabIndexStartingNumber + 4, Int16)
            Me.txtBankBranchName.TabIndex = CType(TabIndexStartingNumber + 5, Int16)
            Me.textboxSwiftCode.TabIndex = CType(TabIndexStartingNumber + 6, Int16)
            Me.textboxIBAN_Number.TabIndex = CType(TabIndexStartingNumber + 7, Int16)
            Me.moAccountTypeDrop.TabIndex = CType(TabIndexStartingNumber + 8, Int16)
            Me.txtTransLimit.TabIndex = CType(TabIndexStartingNumber + 9, Int16)
            Me.txtBankLookupCode.TabIndex = CType(TabIndexStartingNumber + 10, Int16)
            Me.txtBankSubcode.TabIndex = CType(TabIndexStartingNumber + 11, Int16)
            Me.txtBankSortCode.TabIndex = CType(TabIndexStartingNumber + 12, Int16)
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
        ' Move to BankInfo BO 02/08/2008
        Me.Page.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Public Sub PopulateBOFromControl(Optional ByVal blnExcludeSave As Boolean = False, Optional ByVal blnValidate As Boolean = True)
        Dim guidTemp As Guid, dTemp As DecimalType
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

                If .IsNew AndAlso .BankName <> txtBankName.Text Then State.IsNewObjDirty = True
                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo, "BankName", txtBankName)

                If .IsNew AndAlso .BranchName <> txtBankBranchName.Text Then State.IsNewObjDirty = True
                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo, "BranchName", txtBankBranchName)

                If .IsNew AndAlso .BankSortCode <> txtBankSortCode.Text Then State.IsNewObjDirty = True
                Me.Page.PopulateBOProperty(Me.State.myBankInfoBo, "BankSortCode", txtBankSortCode)

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
                Me.Page.PopulateControlFromBOProperty(Me.textboxNameAccount, .Account_Name)
                Me.Page.PopulateControlFromBOProperty(Me.textboxBankAccountNo, .Account_Number)
                Me.Page.PopulateControlFromBOProperty(Me.textboxBankID, .Bank_Id)
                Me.Page.PopulateControlFromBOProperty(Me.textboxIBAN_Number, .IbanNumber)
                Me.Page.PopulateControlFromBOProperty(Me.textboxSwiftCode, .SwiftCode)
                Me.Page.SetSelectedItem(moCountryDrop_WRITE, .CountryID)
                If .AccountTypeId.Equals(System.Guid.Empty) Then
                    moAccountTypeDrop.SelectedIndex = 0
                Else
                    Me.Page.SetSelectedItem(moAccountTypeDrop, .AccountTypeId)
                End If
                Page.PopulateControlFromBOProperty(Me.txtTransLimit, .TransactionLimit)
                Page.PopulateControlFromBOProperty(Me.txtBankLookupCode, .BankLookupCode)
                Page.PopulateControlFromBOProperty(Me.txtBankSubcode, .BankSubCode)
                Page.PopulateControlFromBOProperty(Me.txtBankName, .BankName)
                Page.PopulateControlFromBOProperty(Me.txtBankBranchName, .BranchName)
                Page.PopulateControlFromBOProperty(Me.txtBankSortCode, .BankSortCode)
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
        ControlMgr.SetVisibleControl(Me.Page, labelBankID, True)
        ControlMgr.SetVisibleControl(Me.Page, textboxBankID, True)
        ControlMgr.SetVisibleControl(Me.Page, LabelSpaceBankID, True)
    End Sub

    Private Sub HideBankId()
        ControlMgr.SetVisibleControl(Me.Page, labelBankID, False)
        ControlMgr.SetVisibleControl(Me.Page, textboxBankID, False)
        ControlMgr.SetVisibleControl(Me.Page, LabelSpaceBankID, False)
        textboxBankID.Text = String.Empty
    End Sub

    Private Sub DisplayBankAccNo()
        ControlMgr.SetVisibleControl(Me.Page, labelBankAccountNo, True)
        ControlMgr.SetVisibleControl(Me.Page, textboxBankAccountNo, True)
        ControlMgr.SetVisibleControl(Me.Page, LabelSpaceAccNo, True)
    End Sub

    Private Sub HideBankAccNo()
        ControlMgr.SetVisibleControl(Me.Page, labelBankAccountNo, False)
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
    Private Sub DomesticTransfer()
        'Domestic transfer---display bank Id and bank Account # controls 
        DisplayBankID()
        DisplayBankAccNo()

        'Domestic transfer---hide swift code & Iban number controls
        HideSwiftCode()
        HideIBAN_Number()
        If Not Me.State.myBankInfoBo Is Nothing Then
            Me.State.myBankInfoBo.DomesticTransfer = True
            Me.State.myBankInfoBo.InternationalEUTransfer = False
            Me.State.myBankInfoBo.InternationalTransfer = False
        End If
    End Sub
    Private Sub InternationalEUTransfer()
        'International transfer & Destination is European country---hide bank Id and bank Account # controls 
        HideBankId()
        HideBankAccNo()

        'International transfer & Destination is European country---display swift code & Iban number controls
        DisplaySwiftCode()
        DisplayIBAN_Number()
        If Not Me.State.myBankInfoBo Is Nothing Then
            Me.State.myBankInfoBo.DomesticTransfer = False
            Me.State.myBankInfoBo.InternationalEUTransfer = True
            Me.State.myBankInfoBo.InternationalTransfer = False
        End If
    End Sub

    Private Sub InternationalTransfer()
        'International transfer & Destination is not a European country---display bank Id, bank Account #, Swift Code controls 
        DisplayBankID()
        DisplayBankAccNo()
        DisplaySwiftCode()

        'International transfer & Destination is European country---hide Iban number controls
        HideIBAN_Number()
        If Not Me.State.myBankInfoBo Is Nothing Then
            Me.State.myBankInfoBo.DomesticTransfer = False
            Me.State.myBankInfoBo.InternationalEUTransfer = False
            Me.State.myBankInfoBo.InternationalTransfer = True
        End If
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
