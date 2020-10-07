Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements


Namespace Certificates

    Partial Class CertItemDetailForm
        Inherits ElitaPlusPage

#Region "Page State"

#Region "MyState"
        Class MyState
            Public MyBO As CertItem
            Public ScreenSnapShotBO As CertItem
            Public CertItemId As Guid = Guid.Empty
            Public certificateId As Guid = Guid.Empty
            Public isEdit As Boolean = False
            Public certificateCompanyId As Guid
            Public companyCode As String
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public inputParameters As ReturnType
            Public _moCertificate As Certificate
            Public ChangeEquipmentFlow As Boolean = False
            Public Dealer_Type_code As String = String.Empty
            Public Dealer_UseEquipment As String = String.Empty
        End Class
#End Region

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                If NavController.State Is Nothing Then
                    NavController.State = New MyState
                    Me.State.MyBO = New CertItem(CType(NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ITEM_ID), Guid))
                    Me.State.CertItemId = Me.State.MyBO.Id

                    moCertificate = Me.State.MyBO.GetCertificate(Me.State.MyBO.CertId)
                    Me.State.certificateId = moCertificate.Id
                    Me.State.certificateCompanyId = moCertificate.CompanyId
                    InitializeFromFlowSession()
                End If
                Return CType(NavController.State, MyState)
                'Return CType(MyBase.State, MyState)
            End Get
        End Property

#End Region


#Region "Variables"

        Private mbIsFirstPass As Boolean = True

#End Region


#Region "Properties"

        Public ReadOnly Property UserCertificateCtr() As UserControlCertificateInfo_New
            Get
                If moCertificateInfoController Is Nothing Then
                    moCertificateInfoController = CType(FindControl("moCertificateInfoController"), UserControlCertificateInfo_New)
                End If
                Return moCertificateInfoController
            End Get
        End Property

        Public ReadOnly Property GetCompanyCode() As String
            Get
                Dim companyBO As Company = New Company(State.certificateCompanyId)

                Return companyBO.Code
            End Get

        End Property

        Public Property moCertificate() As Certificate
            Get
                Return State._moCertificate
            End Get
            Set(Value As Certificate)
                State._moCertificate = Value

            End Set
        End Property
#End Region

#Region "Handlers-Init"

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

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As CertItem
            Public HasDataChanged As Boolean
            Public EquipmentId As Guid
            Public Equip_SKU As String
            Public CallingObjName As String
            Public RiskTypeId As Guid
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As CertItem, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub

            Public Sub New(LastOp As DetailPageCommand, Equip_Id As Guid, Equip_SKU As String, hasDataChanged As Boolean, CallingObj_Name As String, RiskType_Id As Guid)
                LastOperation = LastOp
                Me.HasDataChanged = hasDataChanged
                Me.Equip_SKU = Equip_SKU
                EquipmentId = Equip_Id
                RiskTypeId = RiskType_Id
                CallingObjName = CallingObj_Name
            End Sub
        End Class

#End Region

#End Region

#Region "Page Events"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            If mbIsFirstPass = True Then
                mbIsFirstPass = False
            Else
                ' Do not load again the Page that was already loaded
                Return
            End If

            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Certificates")
            UpdateBreadCrum()

            Try
                MasterPage.MessageController.Clear_Hide()
                EnableDisableControls(EditPanel_WRITE, True)

                If Not IsPostBack Then
                    MenuEnabled = False
                    AddCalendar_New(ImageButtonReturnDate, ReplaceReturnDateText_WRITE)
                    AddCalendar_New(ImageButtonFirstUseDate, FirstUseDateText)
                    AddCalendar_New(ImageButtonLastUseDate, LastUseDateText)
                    If State.MyBO Is Nothing Then
                        State.MyBO = New CertItem
                    End If
                    Trace(Me, "CertItem Id=" & GuidControl.GuidToHexString(State.MyBO.Id))
                    State.companyCode = GetCompanyCode
                    PopulateDropdowns()
                    PopulateFormFromBOs()
                    SetDealerFlags()
                    EnableDisableFields()
                End If
                BindBoPropertiesToLabels()
                CheckIfComingFromSaveConfirm()
                ChangeEquipment()             'REQ 1106 Price List
                If Not IsPostBack Then
                    AddLabelDecorations(State.MyBO)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                CleanPopupInput()
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub
#End Region

#Region "Handlers-DropDown"

#End Region

#Region "Handlers-Buttons"

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs)
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.boChanged)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                State.LastErrMsg = MasterPage.MessageController.Text
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs)
            Try
                PopulateFormFromBOs()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"
        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State.MyBO IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                        TranslationBase.TranslateLabelOrMessage("Item")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Certificate") & " " & "Item"
                End If
            End If
        End Sub

        Private Sub BindBoPropertiesToLabels()
            BindBOPropertyToLabel(State.MyBO, "ItemCode", ItemCodeLabel)
            BindBOPropertyToLabel(State.MyBO, "ItemDescription", ItemDescLabel)
            BindBOPropertyToLabel(State.MyBO, "RiskTypeId", RiskTypeLabel)
            BindBOPropertyToLabel(State.MyBO, "ManufacturerId", ManufacturerLabel)
            BindBOPropertyToLabel(State.MyBO, "MaxReplacementCost", MaxReplacementCostLabel)
            BindBOPropertyToLabel(State.MyBO, "SerialNumber", SerialNumberIMEILabel)
            BindBOPropertyToLabel(State.MyBO, "SerialNumber", SerialNumberLabel)
            BindBOPropertyToLabel(State.MyBO, "IMEINumber", IMEINumberLabel)
            BindBOPropertyToLabel(State.MyBO, "Model", ModelLabel)
            BindBOPropertyToLabel(State.MyBO, "ItemRetailPrice", RetailPriceLabel)
            BindBOPropertyToLabel(State.MyBO, "ItemReplaceReturnDate", ReplaceReturnDateLabel)
            BindBOPropertyToLabel(State.MyBO, "ProductCode", ProductCodeLabel)
            BindBOPropertyToLabel(State.MyBO, "MobileType", MobileTypeLabel)
            BindBOPropertyToLabel(State.MyBO, "FirstUseDate", FirstUseDateLabel)
            BindBOPropertyToLabel(State.MyBO, "LastUseDate", LastUseDateLabel)
            BindBOPropertyToLabel(State.MyBO, "SimCardNumber", SimCardNumberLabel)
            BindBOPropertyToLabel(State.MyBO, "SkuNumber", SKUNumberLabel)
            BindBOPropertyToLabel(State.MyBO, "EffectiveDate", EffectiveDateLabel)
            BindBOPropertyToLabel(State.MyBO, "ExpirationDate", ExpirationDateLabel)
            BindBOPropertyToLabel(State.MyBO, "CertProductCode", CertProdCodeLabel)
            BindBOPropertyToLabel(State.MyBO, "OriginalRetailPrice", OriginalRetailPriceLabel)
            BindBOPropertyToLabel(State.MyBO, "AllowedEvents", AllowedEventsLabel)
            BindBOPropertyToLabel(State.MyBO, "MaxInsuredAmount", MaxInsuredAmountLabel)

        End Sub

        Protected Sub PopulateFormFromBOs()
            Try
                moCertificateInfoController = UserCertificateCtr
                moCertificateInfoController.InitController(State.MyBO.CertId, , State.companyCode)
                With State.MyBO
                    PopulateControlFromBOProperty(ItemCodeText, .ItemCode)
                    PopulateControlFromBOProperty(ItemDescText, .ItemDescription)
                    SetSelectedItem(cboRiskTypeId, .RiskTypeId)
                    SetSelectedItem(cboManufacturerId, .ManufacturerId)
                    PopulateControlFromBOProperty(SerialNumberText, .SerialNumber)
                    PopulateControlFromBOProperty(IMEINumberText, .IMEINumber)
                    PopulateControlFromBOProperty(ModelText, .Model)
                    PopulateControlFromBOProperty(MaxReplacementCostText, .MaxReplacementCost, DECIMAL_FORMAT)
                    PopulateControlFromBOProperty(RetailPriceText, .ItemRetailPrice, DECIMAL_FORMAT)
                    PopulateControlFromBOProperty(ItemNumberText, .ItemNumber)
                    PopulateControlFromBOProperty(ReplaceReturnDateText_WRITE, .ItemReplaceReturnDate)
                    PopulateControlFromBOProperty(txtProductCode, .ProductCode)
                    SetSelectedItem(cboMobileType, LookupListNew.GetIdFromCode("MOB_TYPE", .MobileType))
                    PopulateControlFromBOProperty(FirstUseDateText, .FirstUseDate)
                    PopulateControlFromBOProperty(LastUseDateText, .LastUseDate)
                    PopulateControlFromBOProperty(SimCardNumberText, .SimCardNumber)
                    PopulateControlFromBOProperty(SKUNumberText, .SkuNumber)
                    PopulateControlFromBOProperty(EffectiveDateText, .EffectiveDate)
                    PopulateControlFromBOProperty(ExpirationDateText, .ExpirationDate)
                    PopulateControlFromBOProperty(CertProdCodeText, .CertProductCode)
                    PopulateControlFromBOProperty(OriginalRetailPriceText, .OriginalRetailPrice, DECIMAL_FORMAT)
                    PopulateControlFromBOProperty(AllowedEventsText, .AllowedEvents)
                    PopulateControlFromBOProperty(MaxInsuredAmountText, .MaxInsuredAmount)

                    If (State.MyBO.Cert.Product.BenefitEligibleFlagXCD = Codes.EXT_YESNO_Y) Then
                        trBenefitCheck.Visible = True
                        PopulateControlFromBOProperty(BenefitStatusCheckText, .BenefitStatus)
                        If (.BenefitStatus IsNot Nothing) Then
                            If (.BenefitStatus.ToUpperInvariant() = Codes.BENEFIT_STATUS__INELIGIBLE.ToUpperInvariant()) Then
                                PopulateControlFromBOProperty(IneligibleReasonText, .IneligibilityReason)
                                lblIneligibleReason.Visible = True
                                IneligibleReasonText.Visible = True
                            Else
                                lblIneligibleReason.Visible = False
                                IneligibleReasonText.Visible = False
                            End If
                        Else
                            lblIneligibleReason.Visible = False
                            IneligibleReasonText.Visible = False
                        End If
                    Else
                        trBenefitCheck.Visible = False
                    End If
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateDropdowns()
            Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim riskTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            cboRiskTypeId.Populate(riskTypeLkl, New PopulateOptions() With
             {
               .AddBlankItem = False
                })

            Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            cboManufacturerId.Populate(manufacturerLkl, New PopulateOptions() With
            {
              .AddBlankItem = True
                })

            cboMobileType.Populate(CommonConfigManager.Current.ListManager.GetList("MOB_TYPE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
             {
              .AddBlankItem = True
             })

        End Sub

        Private Sub PopulateBOsFromForm()
            PopulateBOProperty(State.MyBO, "ItemCode", ItemCodeText)
            PopulateBOProperty(State.MyBO, "ItemDescription", ItemDescText)
            PopulateBOProperty(State.MyBO, "ItemNumber", ItemNumberText)
            PopulateBOProperty(State.MyBO, "RiskTypeId", cboRiskTypeId)
            PopulateBOProperty(State.MyBO, "ManufacturerId", cboManufacturerId)
            PopulateBOProperty(State.MyBO, "MaxReplacementCost", MaxReplacementCostText)
            PopulateBOProperty(State.MyBO, "SerialNumber", SerialNumberText)
            PopulateBOProperty(State.MyBO, "IMEINumber", IMEINumberText)
            If State.MyBO.Cert.Dealer.ImeiUseXcd IsNot Nothing AndAlso State.MyBO.Cert.Dealer.ImeiUseXcd.Equals("IMEI_USE_LST-NOTINUSE") Then
                PopulateBOProperty(State.MyBO, "IMEINumber", SerialNumberText)
            End If
            PopulateBOProperty(State.MyBO, "Model", ModelText)
            PopulateBOProperty(State.MyBO, "ItemRetailPrice", RetailPriceText)
            PopulateBOProperty(State.MyBO, "ItemReplaceReturnDate", ReplaceReturnDateText_WRITE)
            PopulateBOProperty(State.MyBO, "ProductCode", txtProductCode)
            PopulateBOProperty(State.MyBO, "MobileType", LookupListNew.GetCodeFromId("MOB_TYPE", GetSelectedItem(cboMobileType)))  'Me.cboMobileType) 
            PopulateBOProperty(State.MyBO, "FirstUseDate", FirstUseDateText)
            PopulateBOProperty(State.MyBO, "LastUseDate", LastUseDateText)
            PopulateBOProperty(State.MyBO, "SimCardNumber", SimCardNumberText)
            PopulateBOProperty(State.MyBO, "SkuNumber", SKUNumberText)
            PopulateBOProperty(State.MyBO, "EffectiveDate", EffectiveDateText)
            PopulateBOProperty(State.MyBO, "ExpirationDate", ExpirationDateText)
            PopulateBOProperty(State.MyBO, "CertProductCode", CertProdCodeText)
            PopulateBOProperty(State.MyBO, "OriginalRetailPrice", OriginalRetailPriceText)
            PopulateBOProperty(State.MyBO, "AllowedEvents", AllowedEventsText)
            PopulateBOProperty(State.MyBO, "MaxInsuredAmount", MaxInsuredAmountText)


        End Sub

        ' Clean Popup Input
        Private Sub CleanPopupInput()
            Try
                If State IsNot Nothing Then
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    HiddenSaveChangesPromptResponse.Value = ""
                End If
            Catch ex As Exception

            End Try

        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            Dim lastAction As ElitaPlusPage.DetailPageCommand = State.ActionInProgress
            'Clean after consuming the action
            CleanPopupInput()

            Try
                If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                    If lastAction <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                        State.MyBO.Save()
                    End If
                    Select Case lastAction
                        Case ElitaPlusPage.DetailPageCommand.Back
                            State.boChanged = True
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.boChanged)
                            NavController.Navigate(Me, "back", retObj)
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.boChanged)
                            NavController.Navigate(Me, "back", retObj)
                    End Select
                ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                    Select Case lastAction
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.boChanged)
                            NavController.Navigate(Me, "back", retObj)
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)
                    End Select
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub EnableDisableFields()
            Try
                btnBack.Enabled = True

                If Not State.isEdit Then
                    ControlMgr.SetEnableControl(Me, btnEdit_WRITE, True)
                    ControlMgr.SetEnableControl(Me, btnHistory_WRITE, True)
                    ControlMgr.SetEnableControl(Me, btnUndo_WRITE, False)
                    ControlMgr.SetEnableControl(Me, btnApply_WRITE, False)
                Else 'Edit Mode
                    ControlMgr.SetEnableControl(Me, btnUndo_WRITE, True)
                    ControlMgr.SetEnableControl(Me, btnApply_WRITE, True)
                    ControlMgr.SetEnableControl(Me, btnEdit_WRITE, False)
                    ItemCodeText.Enabled = True
                    'Me.ItemCodeText.ReadOnly = False
                    cboManufacturerId.Enabled = True
                    cboRiskTypeId.Enabled = True
                    SerialNumberText.ReadOnly = False
                    SerialNumberText.BackColor = Color.White
                    IMEINumberText.ReadOnly = False
                    RetailPriceText.ReadOnly = False
                    ItemDescText.ReadOnly = False
                    MaxReplacementCostText.ReadOnly = False
                    ModelText.ReadOnly = False
                    SKUNumberText.ReadOnly = False
                    ReplaceReturnDateText_WRITE.ReadOnly = False
                    ImageButtonReturnDate.Enabled = True
                    txtProductCode.ReadOnly = False
                    FirstUseDateText.ReadOnly = False
                    LastUseDateText.ReadOnly = False
                    SimCardNumberText.ReadOnly = False
                    cboMobileType.Enabled = True
                    ImageButtonFirstUseDate.Enabled = True
                    ImageButtonLastUseDate.Enabled = True
                    OriginalRetailPriceText.ReadOnly = False
                    AllowedEventsText.ReadOnly = True
                    MaxInsuredAmountText.ReadOnly = True
                End If
                If State.Dealer_UseEquipment = Codes.YESNO_Y Then
                    ControlMgr.SetVisibleControl(Me, btnChangeEquipment_WRITE, Not State.isEdit)
                End If
                If Not State.Dealer_Type_code = Codes.DEALER_TYPE_CODE_WEPP Then
                    'if the dealer type is not wireless then some fields will be hidden
                    MobileTypeLabel.Visible = False
                    cboMobileType.Visible = False
                    FirstUseDateLabel.Visible = False
                    FirstUseDateText.Visible = False
                    SimCardNumberLabel.Visible = False
                    SimCardNumberText.Visible = False
                    LastUseDateLabel.Visible = False
                    LastUseDateText.Visible = False
                    ImageButtonLastUseDate.Visible = False
                    SKUNumberLabel.Visible = False
                    SKUNumberText.Visible = False
                    ImageButtonFirstUseDate.Visible = False
                    ImageButtonLastUseDate.Visible = False
                End If

                If Not State.MyBO.Cert.Dealer.ImeiUseXcd.Equals("IMEI_USE_LST-NOTINUSE") Then
                    SerialNumberIMEILabel.Visible = False
                    SerialNumberLabel.Visible = True
                    IMEINumberLabel.Visible = True
                    IMEINumberText.Visible = True
                Else
                    SerialNumberLabel.Visible = False
                    SerialNumberIMEILabel.Visible = True
                    IMEINumberLabel.Visible = False
                    IMEINumberText.Visible = False
                End If
                If LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, State.MyBO.Cert.Dealer.DealerTypeId) = Codes.DEALER_TYPES__VSC Then
                    trVSCOnly.Visible = True
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub InitializeFromFlowSession()
            State.inputParameters = CType(NavController.ParametersPassed, ReturnType)
        End Sub

        Protected Sub ChangeEquipment()
            Try
                'check the input parameter and begin equipment change if a different equipment is selected
                If State.inputParameters IsNot Nothing Then
                    With State.inputParameters
                        If .CallingObjName = "equipment_selected" Then
                            If Not State.MyBO.EquipmentId = .EquipmentId Then
                                State.MyBO.BeginEdit()
                                Dim Equip As New Equipment(.EquipmentId)
                                PopulateBOProperty(State.MyBO, "EquipmentId", Equip.Id)
                                PopulateBOProperty(State.MyBO, "Model", Equip.Model)
                                PopulateBOProperty(State.MyBO, "ManufacturerId", Equip.ManufacturerId)
                                PopulateBOProperty(State.MyBO, "SkuNumber", .Equip_SKU)
                                PopulateBOProperty(State.MyBO, "ItemDescription", Equip.Description)

                                If Not .RiskTypeId = Guid.Empty Then
                                    If Not State.MyBO.RiskTypeId = .RiskTypeId Then
                                        'Me.BindListControlToDataView(Me.cboRiskTypeId, _
                                        '                             Me.State.MyBO.GetRiskTypesByRiskGroup(.RiskTypeId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id) _
                                        ''   , , , False)
                                        'SHOW THE USER A WARNING MESSAGE THAT RISK TYPE IS CHANGED 

                                    End If
                                End If
                                'Now refresh the change done to BO and enable the buttons 
                                'refresh frorm from bo
                                State.MyBO.EndEdit()
                                PopulateFormFromBOs()
                                Change_equipment_toggle(False)
                                State.inputParameters = Nothing
                            End If
                        End If
                    End With
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub Change_equipment_toggle(toggle As Boolean)
            'button 
            If State.Dealer_UseEquipment = Codes.YESNO_Y Then
                ControlMgr.SetVisibleControl(Me, btnChangeEquipment_WRITE, toggle)
            End If
            btnEdit_WRITE.Enabled = toggle
            btnHistory_WRITE.Enabled = toggle
            btnApply_WRITE.Enabled = Not toggle
            btnUndo_WRITE.Enabled = Not toggle

            'when equipment change is in progress toggle comes as false thats why NOt toggle
            State.ChangeEquipmentFlow = Not toggle
            State.isEdit = Not toggle
        End Sub

        Private Sub SetDealerFlags()
            Try
                Dim oDealer As Dealer
                oDealer = New Dealer(moCertificate.DealerId)
                State.Dealer_Type_code = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, oDealer.DealerTypeId)
                State.Dealer_UseEquipment = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, oDealer.UseEquipmentId)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Button Clicks"
        Private Sub btnApply_WRITE_Click1(sender As Object, e As System.EventArgs) Handles btnApply_WRITE.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    If SerialNumberText.Text <> "" Or IMEINumberText.Text <> "" Then
                        Dim dv As DataView ', oCertItem As CertItem 
                        Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                        If SerialNumberText.Text <> String.Empty Then
                            dv = CertItem.ValidateSerialNumber(SerialNumberText.Text, moCertificate.CertNumber, compGroupId)
                            If dv.Count > 0 Then
                                If SerialNumberLabel.Visible Then
                                    ElitaPlusPage.SetLabelError(SerialNumberLabel)
                                ElseIf SerialNumberIMEILabel.Visible Then
                                    ElitaPlusPage.SetLabelError(SerialNumberIMEILabel)
                                End If
                                Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_VIN_NUMBER_ERR)
                            End If
                        ElseIf IMEINumberText.Text <> String.Empty Then
                            dv = CertItem.ValidateSerialNumber(IMEINumberText.Text, moCertificate.CertNumber, compGroupId)
                            If dv.Count > 0 Then
                                ElitaPlusPage.SetLabelError(IMEINumberLabel)
                                Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_VIN_NUMBER_ERR)
                            End If
                        End If

                        If (moCertificate.Source.ToUpper() = "VSC") Then
                            If (SerialNumberText.Text.Length > 8) Then
                                moCertificate.VinLocator = SerialNumberText.Text.Substring(SerialNumberText.Text.Length - 8)
                            Else
                                If SerialNumberText.Text <> String.Empty Then
                                    moCertificate.VinLocator = SerialNumberText.Text
                                ElseIf IMEINumberText.Text <> String.Empty Then
                                    moCertificate.VinLocator = IMEINumberText.Text
                                End If
                            End If
                        End If
                    End If

                    SetLabelColor(SerialNumberLabel)
                    SetLabelColor(IMEINumberLabel)
                    SetLabelColor(SerialNumberIMEILabel)
                    State.MyBO.Save()
                    State.HasDataChanged = True
                    PopulateFormFromBOs()
                    State.boChanged = True
                    State.isEdit = False
                    EnableDisableFields()
                    'DEF-22768-START
                    'Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                    'DEF-22768-END
                    If State.ChangeEquipmentFlow Then Change_equipment_toggle(True)
                Else
                    'DEF-22768-START
                    'Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    MasterPage.MessageController.AddSuccess(Message.MSG_RECORD_NOT_SAVED, True)
                    'DEF-22768-END
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                State.isEdit = True
                EnableDisableFields()
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click1(sender As Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If State.ChangeEquipmentFlow Then Change_equipment_toggle(True)
                State.MyBO = New CertItem(State.MyBO.Id)
                PopulateFormFromBOs()
                State.isEdit = False
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnBack_Click1(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Try
                moCertificate = State.MyBO.GetCertificate(State.MyBO.CertId)
                PopulateBOsFromForm()
                If State.MyBO.IsFamilyDirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.boChanged)
                    NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                State.LastErrMsg = MasterPage.MessageController.Text
            End Try

        End Sub

        Public Shared Sub SetLabelColor(lbl As Label)
            lbl.ForeColor = Color.Black
        End Sub

        Private Sub btnEdit_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnEdit_WRITE.Click
            Try
                State.isEdit = True
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnHistory_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnHistory_WRITE.Click
            Try
                NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = State._moCertificate
                NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ITEM_ID) = State.CertItemId
                NavController.Navigate(Me, "item_history_selected")
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnChangeEquipment_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnChangeEquipment_WRITE.Click
            Try
                NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = State._moCertificate
                NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ITEM_ID) = State.CertItemId
                NavController.Navigate(Me, "Change_equipment_selected")
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

    End Class

End Namespace
