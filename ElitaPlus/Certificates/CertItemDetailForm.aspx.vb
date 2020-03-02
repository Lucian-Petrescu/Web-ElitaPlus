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
                If Me.NavController.State Is Nothing Then
                    Me.NavController.State = New MyState
                    Me.State.MyBO = New CertItem(CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ITEM_ID), Guid))
                    Me.State.CertItemId = Me.State.MyBO.Id

                    moCertificate = Me.State.MyBO.GetCertificate(Me.State.MyBO.CertId)
                    Me.State.certificateId = moCertificate.Id
                    Me.State.certificateCompanyId = moCertificate.CompanyId
                    InitializeFromFlowSession()
                End If
                Return CType(Me.NavController.State, MyState)
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
                Dim companyBO As Company = New Company(Me.State.certificateCompanyId)

                Return companyBO.Code
            End Get

        End Property

        Public Property moCertificate() As Certificate
            Get
                Return Me.State._moCertificate
            End Get
            Set(ByVal Value As Certificate)
                Me.State._moCertificate = Value

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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As CertItem, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub

            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal Equip_Id As Guid, ByVal Equip_SKU As String, ByVal hasDataChanged As Boolean, CallingObj_Name As String, RiskType_Id As Guid)
                Me.LastOperation = LastOp
                Me.HasDataChanged = hasDataChanged
                Me.Equip_SKU = Equip_SKU
                Me.EquipmentId = Equip_Id
                Me.RiskTypeId = RiskType_Id
                Me.CallingObjName = CallingObj_Name
            End Sub
        End Class

#End Region

#End Region

#Region "Page Events"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If mbIsFirstPass = True Then
                mbIsFirstPass = False
            Else
                ' Do not load again the Page that was already loaded
                Return
            End If

            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Certificates")
            Me.UpdateBreadCrum()

            Try
                Me.MasterPage.MessageController.Clear_Hide()
                EnableDisableControls(Me.EditPanel_WRITE, True)

                If Not Me.IsPostBack Then
                    Me.MenuEnabled = False
                    Me.AddCalendar_New(Me.ImageButtonReturnDate, Me.ReplaceReturnDateText_WRITE)
                    Me.AddCalendar_New(Me.ImageButtonFirstUseDate, Me.FirstUseDateText)
                    Me.AddCalendar_New(Me.ImageButtonLastUseDate, Me.LastUseDateText)
                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New CertItem
                    End If
                    Trace(Me, "CertItem Id=" & GuidControl.GuidToHexString(Me.State.MyBO.Id))
                    Me.State.companyCode = GetCompanyCode
                    PopulateDropdowns()
                    Me.PopulateFormFromBOs()
                    Me.SetDealerFlags()
                    Me.EnableDisableFields()
                End If
                BindBoPropertiesToLabels()
                CheckIfComingFromSaveConfirm()
                ChangeEquipment()             'REQ 1106 Price List
                If Not Me.IsPostBack Then
                    Me.AddLabelDecorations(Me.State.MyBO)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                CleanPopupInput()
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub
#End Region

#Region "Handlers-DropDown"

#End Region

#Region "Handlers-Buttons"

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.boChanged)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Try
                PopulateFormFromBOs()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"
        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State.MyBO Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                        TranslationBase.TranslateLabelOrMessage("Item")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Certificate") & " " & "Item"
                End If
            End If
        End Sub

        Private Sub BindBoPropertiesToLabels()
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ItemCode", Me.ItemCodeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ItemDescription", Me.ItemDescLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "RiskTypeId", Me.RiskTypeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ManufacturerId", Me.ManufacturerLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "MaxReplacementCost", Me.MaxReplacementCostLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "SerialNumber", Me.SerialNumberIMEILabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "SerialNumber", Me.SerialNumberLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "IMEINumber", Me.IMEINumberLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Model", Me.ModelLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ItemRetailPrice", Me.RetailPriceLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ItemReplaceReturnDate", Me.ReplaceReturnDateLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ProductCode", Me.ProductCodeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "MobileType", Me.MobileTypeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "FirstUseDate", Me.FirstUseDateLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "LastUseDate", Me.LastUseDateLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "SimCardNumber", Me.SimCardNumberLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "SkuNumber", Me.SKUNumberLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "EffectiveDate", Me.EffectiveDateLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ExpirationDate", Me.ExpirationDateLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CertProductCode", Me.CertProdCodeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "OriginalRetailPrice", Me.OriginalRetailPriceLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "AllowedEvents", Me.AllowedEventsLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "MaxInsuredAmount", Me.MaxInsuredAmountLabel)

        End Sub

        Protected Sub PopulateFormFromBOs()
            Try
                moCertificateInfoController = Me.UserCertificateCtr
                moCertificateInfoController.InitController(Me.State.MyBO.CertId, , Me.State.companyCode)
                With Me.State.MyBO
                    Me.PopulateControlFromBOProperty(Me.ItemCodeText, .ItemCode)
                    Me.PopulateControlFromBOProperty(Me.ItemDescText, .ItemDescription)
                    Me.SetSelectedItem(Me.cboRiskTypeId, .RiskTypeId)
                    Me.SetSelectedItem(Me.cboManufacturerId, .ManufacturerId)
                    Me.PopulateControlFromBOProperty(Me.SerialNumberText, .SerialNumber)
                    Me.PopulateControlFromBOProperty(Me.IMEINumberText, .IMEINumber)
                    Me.PopulateControlFromBOProperty(Me.ModelText, .Model)
                    Me.PopulateControlFromBOProperty(Me.MaxReplacementCostText, .MaxReplacementCost, Me.DECIMAL_FORMAT)
                    Me.PopulateControlFromBOProperty(Me.RetailPriceText, .ItemRetailPrice, Me.DECIMAL_FORMAT)
                    Me.PopulateControlFromBOProperty(Me.ItemNumberText, .ItemNumber)
                    Me.PopulateControlFromBOProperty(Me.ReplaceReturnDateText_WRITE, .ItemReplaceReturnDate)
                    Me.PopulateControlFromBOProperty(Me.txtProductCode, .ProductCode)
                    Me.SetSelectedItem(Me.cboMobileType, LookupListNew.GetIdFromCode("MOB_TYPE", .MobileType))
                    Me.PopulateControlFromBOProperty(Me.FirstUseDateText, .FirstUseDate)
                    Me.PopulateControlFromBOProperty(Me.LastUseDateText, .LastUseDate)
                    Me.PopulateControlFromBOProperty(Me.SimCardNumberText, .SimCardNumber)
                    Me.PopulateControlFromBOProperty(Me.SKUNumberText, .SkuNumber)
                    Me.PopulateControlFromBOProperty(Me.EffectiveDateText, .EffectiveDate)
                    Me.PopulateControlFromBOProperty(Me.ExpirationDateText, .ExpirationDate)
                    Me.PopulateControlFromBOProperty(Me.CertProdCodeText, .CertProductCode)
                    Me.PopulateControlFromBOProperty(Me.OriginalRetailPriceText, .OriginalRetailPrice, DECIMAL_FORMAT)
                    Me.PopulateControlFromBOProperty(Me.AllowedEventsText, .AllowedEvents)
                    Me.PopulateControlFromBOProperty(Me.MaxInsuredAmountText, .MaxInsuredAmount)

                    If (Me.State.MyBO.Cert.Product.BenefitEligibleFlagXCD = Codes.EXT_YESNO_Y) Then
                        trBenefitCheck.Visible = True
                        Me.PopulateControlFromBOProperty(Me.BenefitStatusCheckText, .BenefitStatus)
                        If (Not .BenefitStatus Is Nothing) Then
                            If (.BenefitStatus.ToUpperInvariant() = Codes.BENEFIT_STATUS__INELIGIBLE.ToUpperInvariant()) Then
                                Me.PopulateControlFromBOProperty(Me.IneligibleReasonText, .IneligibilityReason)
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
            Me.PopulateBOProperty(Me.State.MyBO, "ItemCode", Me.ItemCodeText)
            Me.PopulateBOProperty(Me.State.MyBO, "ItemDescription", Me.ItemDescText)
            Me.PopulateBOProperty(Me.State.MyBO, "ItemNumber", Me.ItemNumberText)
            Me.PopulateBOProperty(Me.State.MyBO, "RiskTypeId", Me.cboRiskTypeId)
            Me.PopulateBOProperty(Me.State.MyBO, "ManufacturerId", Me.cboManufacturerId)
            Me.PopulateBOProperty(Me.State.MyBO, "MaxReplacementCost", Me.MaxReplacementCostText)
            Me.PopulateBOProperty(Me.State.MyBO, "SerialNumber", Me.SerialNumberText)
            Me.PopulateBOProperty(Me.State.MyBO, "IMEINumber", Me.IMEINumberText)
            Me.PopulateBOProperty(Me.State.MyBO, "Model", Me.ModelText)
            Me.PopulateBOProperty(Me.State.MyBO, "ItemRetailPrice", Me.RetailPriceText)
            Me.PopulateBOProperty(Me.State.MyBO, "ItemReplaceReturnDate", Me.ReplaceReturnDateText_WRITE)
            Me.PopulateBOProperty(Me.State.MyBO, "ProductCode", Me.txtProductCode)
            Me.PopulateBOProperty(Me.State.MyBO, "MobileType", LookupListNew.GetCodeFromId("MOB_TYPE", GetSelectedItem(Me.cboMobileType)))  'Me.cboMobileType) 
            Me.PopulateBOProperty(Me.State.MyBO, "FirstUseDate", Me.FirstUseDateText)
            Me.PopulateBOProperty(Me.State.MyBO, "LastUseDate", Me.LastUseDateText)
            Me.PopulateBOProperty(Me.State.MyBO, "SimCardNumber", Me.SimCardNumberText)
            Me.PopulateBOProperty(Me.State.MyBO, "SkuNumber", Me.SKUNumberText)
            Me.PopulateBOProperty(Me.State.MyBO, "EffectiveDate", Me.EffectiveDateText)
            Me.PopulateBOProperty(Me.State.MyBO, "ExpirationDate", Me.ExpirationDateText)
            Me.PopulateBOProperty(Me.State.MyBO, "CertProductCode", Me.CertProdCodeText)
            Me.PopulateBOProperty(Me.State.MyBO, "OriginalRetailPrice", Me.OriginalRetailPriceText)
            Me.PopulateBOProperty(Me.State.MyBO, "AllowedEvents", Me.AllowedEventsText)
            Me.PopulateBOProperty(Me.State.MyBO, "MaxInsuredAmount", Me.MaxInsuredAmountText)


        End Sub

        ' Clean Popup Input
        Private Sub CleanPopupInput()
            Try
                If Not Me.State Is Nothing Then
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    Me.HiddenSaveChangesPromptResponse.Value = ""
                End If
            Catch ex As Exception

            End Try

        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            Dim lastAction As ElitaPlusPage.DetailPageCommand = Me.State.ActionInProgress
            'Clean after consuming the action
            CleanPopupInput()

            Try
                If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                    If lastAction <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                        Me.State.MyBO.Save()
                    End If
                    Select Case lastAction
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.State.boChanged = True
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.boChanged)
                            Me.NavController.Navigate(Me, "back", retObj)
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.boChanged)
                            Me.NavController.Navigate(Me, "back", retObj)
                    End Select
                ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                    Select Case lastAction
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.boChanged)
                            Me.NavController.Navigate(Me, "back", retObj)
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)
                    End Select
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub EnableDisableFields()
            Try
                Me.btnBack.Enabled = True

                If Not Me.State.isEdit Then
                    ControlMgr.SetEnableControl(Me, Me.btnEdit_WRITE, True)
                    ControlMgr.SetEnableControl(Me, Me.btnHistory_WRITE, True)
                    ControlMgr.SetEnableControl(Me, btnUndo_WRITE, False)
                    ControlMgr.SetEnableControl(Me, Me.btnApply_WRITE, False)
                Else 'Edit Mode
                    ControlMgr.SetEnableControl(Me, btnUndo_WRITE, True)
                    ControlMgr.SetEnableControl(Me, Me.btnApply_WRITE, True)
                    ControlMgr.SetEnableControl(Me, Me.btnEdit_WRITE, False)
                    Me.ItemCodeText.Enabled = True
                    'Me.ItemCodeText.ReadOnly = False
                    Me.cboManufacturerId.Enabled = True
                    Me.cboRiskTypeId.Enabled = True
                    Me.SerialNumberText.ReadOnly = False
                    Me.SerialNumberText.BackColor = Color.White
                    Me.IMEINumberText.ReadOnly = False
                    Me.RetailPriceText.ReadOnly = False
                    Me.ItemDescText.ReadOnly = False
                    Me.MaxReplacementCostText.ReadOnly = False
                    Me.ModelText.ReadOnly = False
                    Me.SKUNumberText.ReadOnly = False
                    Me.ReplaceReturnDateText_WRITE.ReadOnly = False
                    Me.ImageButtonReturnDate.Enabled = True
                    Me.txtProductCode.ReadOnly = False
                    Me.FirstUseDateText.ReadOnly = False
                    Me.LastUseDateText.ReadOnly = False
                    Me.SimCardNumberText.ReadOnly = False
                    Me.cboMobileType.Enabled = True
                    Me.ImageButtonFirstUseDate.Enabled = True
                    Me.ImageButtonLastUseDate.Enabled = True
                    Me.OriginalRetailPriceText.ReadOnly = False
                    Me.AllowedEventsText.ReadOnly = True
                    Me.MaxInsuredAmountText.ReadOnly = True
                End If
                If Me.State.Dealer_UseEquipment = Codes.YESNO_Y Then
                    ControlMgr.SetVisibleControl(Me, Me.btnChangeEquipment_WRITE, Not Me.State.isEdit)
                End If
                If Not Me.State.Dealer_Type_code = Codes.DEALER_TYPE_CODE_WEPP Then
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
                    Me.ImageButtonFirstUseDate.Visible = False
                    Me.ImageButtonLastUseDate.Visible = False
                End If

                If Not Me.State.MyBO.Cert.Dealer.ImeiUseXcd.Equals("IMEI_USE_LST-NOTINUSE") Then
                    Me.SerialNumberIMEILabel.Visible = False
                    Me.SerialNumberLabel.Visible = True
                    Me.IMEINumberLabel.Visible = True
                    Me.IMEINumberText.Visible = True
                Else
                    Me.SerialNumberLabel.Visible = False
                    Me.SerialNumberIMEILabel.Visible = True
                    Me.IMEINumberLabel.Visible = False
                    Me.IMEINumberText.Visible = False
                End If
                If LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, Me.State.MyBO.Cert.Dealer.DealerTypeId) = Codes.DEALER_TYPES__VSC Then
                    trVSCOnly.Visible = True
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub InitializeFromFlowSession()
            Me.State.inputParameters = CType(Me.NavController.ParametersPassed, ReturnType)
        End Sub

        Protected Sub ChangeEquipment()
            Try
                'check the input parameter and begin equipment change if a different equipment is selected
                If Not Me.State.inputParameters Is Nothing Then
                    With Me.State.inputParameters
                        If .CallingObjName = "equipment_selected" Then
                            If Not Me.State.MyBO.EquipmentId = .EquipmentId Then
                                Me.State.MyBO.BeginEdit()
                                Dim Equip As New Equipment(.EquipmentId)
                                Me.PopulateBOProperty(Me.State.MyBO, "EquipmentId", Equip.Id)
                                Me.PopulateBOProperty(Me.State.MyBO, "Model", Equip.Model)
                                Me.PopulateBOProperty(Me.State.MyBO, "ManufacturerId", Equip.ManufacturerId)
                                Me.PopulateBOProperty(Me.State.MyBO, "SkuNumber", .Equip_SKU)
                                Me.PopulateBOProperty(Me.State.MyBO, "ItemDescription", Equip.Description)

                                If Not .RiskTypeId = Guid.Empty Then
                                    If Not Me.State.MyBO.RiskTypeId = .RiskTypeId Then
                                        'Me.BindListControlToDataView(Me.cboRiskTypeId, _
                                        '                             Me.State.MyBO.GetRiskTypesByRiskGroup(.RiskTypeId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id) _
                                        ''   , , , False)
                                        'SHOW THE USER A WARNING MESSAGE THAT RISK TYPE IS CHANGED 

                                    End If
                                End If
                                'Now refresh the change done to BO and enable the buttons 
                                'refresh frorm from bo
                                Me.State.MyBO.EndEdit()
                                PopulateFormFromBOs()
                                Change_equipment_toggle(False)
                                Me.State.inputParameters = Nothing
                            End If
                        End If
                    End With
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub Change_equipment_toggle(toggle As Boolean)
            'button 
            If Me.State.Dealer_UseEquipment = Codes.YESNO_Y Then
                ControlMgr.SetVisibleControl(Me, Me.btnChangeEquipment_WRITE, toggle)
            End If
            btnEdit_WRITE.Enabled = toggle
            btnHistory_WRITE.Enabled = toggle
            btnApply_WRITE.Enabled = Not toggle
            btnUndo_WRITE.Enabled = Not toggle

            'when equipment change is in progress toggle comes as false thats why NOt toggle
            Me.State.ChangeEquipmentFlow = Not toggle
            Me.State.isEdit = Not toggle
        End Sub

        Private Sub SetDealerFlags()
            Try
                Dim oDealer As Dealer
                oDealer = New Dealer(moCertificate.DealerId)
                Me.State.Dealer_Type_code = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, oDealer.DealerTypeId)
                Me.State.Dealer_UseEquipment = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, oDealer.UseEquipmentId)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Button Clicks"
        Private Sub btnApply_WRITE_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty Then
                    If Me.SerialNumberText.Text <> "" Or Me.IMEINumberText.Text <> "" Then
                        Dim dv As DataView ', oCertItem As CertItem 
                        Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                        If Me.SerialNumberText.Text <> String.Empty Then
                            dv = CertItem.ValidateSerialNumber(Me.SerialNumberText.Text, moCertificate.CertNumber, compGroupId)
                            If dv.Count > 0 Then
                                If Me.SerialNumberLabel.Visible Then
                                    ElitaPlusPage.SetLabelError(Me.SerialNumberLabel)
                                ElseIf Me.SerialNumberIMEILabel.Visible Then
                                    ElitaPlusPage.SetLabelError(Me.SerialNumberIMEILabel)
                                End If
                                Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_VIN_NUMBER_ERR)
                            End If
                        ElseIf Me.IMEINumberText.Text <> String.Empty Then
                            dv = CertItem.ValidateSerialNumber(Me.IMEINumberText.Text, moCertificate.CertNumber, compGroupId)
                            If dv.Count > 0 Then
                                ElitaPlusPage.SetLabelError(Me.IMEINumberLabel)
                                Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_VIN_NUMBER_ERR)
                            End If
                        End If

                        If (Me.moCertificate.Source.ToUpper() = "VSC") Then
                            If (Me.SerialNumberText.Text.Length > 8) Then
                                Me.moCertificate.VinLocator = Me.SerialNumberText.Text.Substring(Me.SerialNumberText.Text.Length - 8)
                            Else
                                If Me.SerialNumberText.Text <> String.Empty Then
                                    Me.moCertificate.VinLocator = Me.SerialNumberText.Text
                                ElseIf Me.IMEINumberText.Text <> String.Empty Then
                                    Me.moCertificate.VinLocator = Me.IMEINumberText.Text
                                End If
                            End If
                        End If
                    End If

                    SetLabelColor(Me.SerialNumberLabel)
                    SetLabelColor(Me.IMEINumberLabel)
                    SetLabelColor(Me.SerialNumberIMEILabel)
                    Me.State.MyBO.Save()
                    Me.State.HasDataChanged = True
                    Me.PopulateFormFromBOs()
                    Me.State.boChanged = True
                    Me.State.isEdit = False
                    Me.EnableDisableFields()
                    'DEF-22768-START
                    'Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                    'DEF-22768-END
                    If Me.State.ChangeEquipmentFlow Then Change_equipment_toggle(True)
                Else
                    'DEF-22768-START
                    'Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.MasterPage.MessageController.AddSuccess(Message.MSG_RECORD_NOT_SAVED, True)
                    'DEF-22768-END
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Me.State.isEdit = True
                Me.EnableDisableFields()
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Me.State.ChangeEquipmentFlow Then Change_equipment_toggle(True)
                Me.State.MyBO = New CertItem(Me.State.MyBO.Id)
                Me.PopulateFormFromBOs()
                Me.State.isEdit = False
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnBack_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                moCertificate = Me.State.MyBO.GetCertificate(Me.State.MyBO.CertId)
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsFamilyDirty Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.boChanged)
                    Me.NavController.Navigate(Me, "back", retObj) 'arf 12-20-04  
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
            End Try

        End Sub

        Public Shared Sub SetLabelColor(ByVal lbl As Label)
            lbl.ForeColor = Color.Black
        End Sub

        Private Sub btnEdit_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit_WRITE.Click
            Try
                Me.State.isEdit = True
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnHistory_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory_WRITE.Click
            Try
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = Me.State._moCertificate
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ITEM_ID) = Me.State.CertItemId
                Me.NavController.Navigate(Me, "item_history_selected")
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnChangeEquipment_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnChangeEquipment_WRITE.Click
            Try
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = Me.State._moCertificate
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ITEM_ID) = Me.State.CertItemId
                Me.NavController.Navigate(Me, "Change_equipment_selected")
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

    End Class

End Namespace
