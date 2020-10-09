Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.Security
Imports Microsoft.VisualBasic
Namespace Certificates

    Partial Class CertRegItemDetailForm
        Inherits ElitaPlusPage

#Region "Page State"

#Region "MyState"
        Class MyState
            Public MyBO As CertRegisteredItem
            Public ScreenSnapShotBO As CertRegisteredItem
            Public CertRegisteredItemId As Guid = Guid.Empty
            Public CertItemId As Guid = Guid.Empty
            Public certificateId As Guid = Guid.Empty
            Public isEdit As Boolean = False
            Public certificateCompanyId As Guid
            Public certificateCompany As String
            Public certNumber As String
            Public companyCode As String
            Public InitialMaufacturer As String

            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public inputParameters As ReturnType
            Public _moCertificate As Certificate
        End Class
#End Region

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                If NavController.State Is Nothing Then
                    NavController.State = New MyState
                    Me.State.MyBO = New CertRegisteredItem(CType(NavController.FlowSession(FlowSessionKeys.SESSION_CERT_REGISTERED_ITEM_ID), Guid))
                    Me.State.CertRegisteredItemId = Me.State.MyBO.Id
                    moCertificate = Me.State.MyBO.GetCertificate(Me.State.MyBO.CertId)
                    Me.State.certificateId = moCertificate.Id
                    Me.State.certificateCompanyId = moCertificate.CompanyId

                    InitializeFromFlowSession()
                End If
                Return CType(NavController.State, MyState)

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

        Public Property moCertificate() As Certificate
            Get
                Return State._moCertificate
            End Get
            Set(Value As Certificate)
                State._moCertificate = Value

            End Set
        End Property

        Public ReadOnly Property GetCompanyCode() As String
            Get
                Dim companyBO As Company = New Company(State.certificateCompanyId)
                Return companyBO.Code
            End Get

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
            Public EditingBo As CertRegisteredItem
            Public HasDataChanged As Boolean
            Public CallingObjName As String
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As CertRegisteredItem, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                hasDataChanged = hasDataChanged
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

                If Not IsPostBack Then
                    MenuEnabled = False
                    AddCalendar_New(ImageButtonPurchasedDate, PurchasedDateText_WRITE)
                    'REQ-6002
                    AddCalendar_New(RegistrationDateImageButton, RegistrationDateText)
                    If State.MyBO Is Nothing Then
                        State.MyBO = New CertRegisteredItem
                    End If
                    Trace(Me, "CertRegisteredItem Id=" & GuidControl.GuidToHexString(State.MyBO.Id))
                    State.companyCode = GetCompanyCode
                    PopulateDropdowns()
                    PopulateFormFromBOs()
                    EnableDisableFields()
                End If
                BindBoPropertiesToLabels()
                CheckIfComingFromSaveConfirm()
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
                If State.MyBO.IsDirty AndAlso State.MyBO.DirtyColumns.Count > 1 Then
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
                    TranslationBase.TranslateLabelOrMessage("Register_Item")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Certificate") & " " & "Register Item"
                End If
            End If
        End Sub

        Private Sub BindBoPropertiesToLabels()
            BindBOPropertyToLabel(State.MyBO, "ItemDescription", ItemDescLabel)
            BindBOPropertyToLabel(State.MyBO, "Manufacturer", ManufacturerLabel)
            BindBOPropertyToLabel(State.MyBO, "SerialNumber", SerialNumberLabel)
            BindBOPropertyToLabel(State.MyBO, "Model", ModelLabel)
            BindBOPropertyToLabel(State.MyBO, "PurchasePrice", PurchasePriceLabel)
            BindBOPropertyToLabel(State.MyBO, "PurchasedDate", PurchasedDateLabel)
            BindBOPropertyToLabel(State.MyBO, "RegisteredItemName", RegItemNameLabel)
            BindBOPropertyToLabel(State.MyBO, "ItemStatus", ItemRegistrationStatusLabel)
            'REQ-6002
            BindBOPropertyToLabel(State.MyBO, "RetailPrice", RetailPriceLabel)
            BindBOPropertyToLabel(State.MyBO, "RegistrationDate", RegistrationDateLabel)
            BindBOPropertyToLabel(State.MyBO, "IndixIDDate", IndixIDLabel)
            BindBOPropertyToLabel(State.MyBO, "ExpirationDate", ExpirationDateLabel)
            ClearGridHeadersAndLabelsErrSign()
        End Sub

        Protected Sub PopulateFormFromBOs()
            Try
                moCertificateInfoController = UserCertificateCtr
                moCertificateInfoController.InitController(State.certificateId, , State.companyCode)
                With State.MyBO
                    PopulateControlFromBOProperty(RegItemNameText, .RegisteredItemName)
                    PopulateControlFromBOProperty(ItemDescText, .ItemDescription)
                    PopulateControlFromBOProperty(DeviceTypeText, .getDeviceTypeDesc(ElitaPlusIdentity.Current.ActiveUser.LanguageId, .DeviceTypeId))

                    SetSelectedItem(cboManufacturerId, .ManufacturerId)
                    If Not .ManufacturerId.Equals(Guid.Empty) Then
                        ControlMgr.SetVisibleControl(Me, ManufacturerTextBox, False)
                    End If
                    BindSelectItem(.ItemStatus, cboItemRegistrationStatus)
                    PopulateControlFromBOProperty(ManufacturerTextBox, .Manufacturer)
                    State.InitialMaufacturer = .Manufacturer

                    If cboManufacturerId.SelectedItem.Text.ToUpper() = "OTHER" Then
                        State.InitialMaufacturer = .Manufacturer
                    Else
                        State.InitialMaufacturer = cboManufacturerId.SelectedItem.Text
                    End If

                    PopulateControlFromBOProperty(SerialNumberText, .SerialNumber)
                    PopulateControlFromBOProperty(ModelText, .Model)
                    PopulateControlFromBOProperty(PurchasedDateText_WRITE, .PurchasedDate)
                    PopulateControlFromBOProperty(PurchasePriceText, .PurchasePrice)
                    'REQ-6002
                    PopulateControlFromBOProperty(RetailPriceText, .RetailPrice)
                    PopulateControlFromBOProperty(RegistrationDateText, .RegistrationDate)
                    PopulateControlFromBOProperty(IndixIDText, .IndixID)
                    PopulateControlFromBOProperty(ExpirationDateText, .ExpirationDate)
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateDropdowns()
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim oManufacturerList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ManufacturerByCompanyGroup", context:=oListContext)
            cboManufacturerId.Populate(oManufacturerList, New PopulateOptions() With
                                        {
                                        .AddBlankItem = True
                                        })
            cboItemRegistrationStatus.PopulateOld("ITEM_REGISTRATION_STATUS", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)

        End Sub

        Private Sub PopulateBOsFromForm()
            PopulateBOProperty(State.MyBO, "RegisteredItemName", RegItemNameText)
            PopulateBOProperty(State.MyBO, "ItemDescription", ItemDescText)
            If cboManufacturerId.SelectedItem.Text.ToUpper() = "OTHER" Then
                PopulateBOProperty(State.MyBO, "Manufacturer", ManufacturerTextBox)
            Else
                PopulateBOProperty(State.MyBO, "Manufacturer", cboManufacturerId.SelectedItem.Text)
            End If
            PopulateBOProperty(State.MyBO, "SerialNumber", SerialNumberText)
            PopulateBOProperty(State.MyBO, "Model", ModelText)
            PopulateBOProperty(State.MyBO, "PurchasedDate", PurchasedDateText_WRITE)
            PopulateBOProperty(State.MyBO, "PurchasePrice", PurchasePriceText)
            PopulateBOProperty(State.MyBO, "ItemStatus", cboItemRegistrationStatus, False, True)
            'REQ-6002
            PopulateBOProperty(State.MyBO, "RetailPrice", RetailPriceText)
            PopulateBOProperty(State.MyBO, "RegistrationDate", RegistrationDateText)
            PopulateBOProperty(State.MyBO, "IndixID", IndixIDText)
            PopulateBOProperty(State.MyBO, "ExpirationDate", ExpirationDateText)
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
                        Dim ErrMsg As New Collections.Generic.List(Of String), CertRegItemID As Guid
                        ValidateFields()
                        PopulateBOsFromForm()
                        If State.MyBO.UpdateRegisterItem(ErrMsg, CertRegItemID) Then
                            State.HasDataChanged = True
                            PopulateFormFromBOs()
                            State.boChanged = True
                            State.isEdit = False
                            EnableDisableFields()
                            MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                        Else
                            MasterPage.MessageController.AddErrorAndShow(ErrMsg.ToArray, False)
                            State.isEdit = True
                            EnableDisableFields()
                        End If
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
                ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_CANCEL Then
                    State.isEdit = True
                    EnableDisableFields()
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
                    SerialNumberText.ReadOnly = True
                    cboManufacturerId.Enabled = False
                    cboItemRegistrationStatus.Enabled = False
                    ManufacturerTextBox.ReadOnly = True
                    DeviceTypeText.ReadOnly = True
                    PurchasePriceText.ReadOnly = True
                    ItemDescText.ReadOnly = True
                    ModelText.ReadOnly = True
                    RegItemNameText.ReadOnly = True
                    PurchasedDateText_WRITE.ReadOnly = True
                    ImageButtonPurchasedDate.Enabled = False
                    'REQ-6002
                    RetailPriceText.ReadOnly = True
                    RegistrationDateText.ReadOnly = True
                    RegistrationDateImageButton.Enabled = False
                    ExpirationDateText.ReadOnly = True
                    IndixIDText.ReadOnly = True
                Else 'Edit Mode
                    ControlMgr.SetEnableControl(Me, btnUndo_WRITE, True)
                    ControlMgr.SetEnableControl(Me, btnApply_WRITE, True)
                    ControlMgr.SetEnableControl(Me, btnEdit_WRITE, False)
                    SerialNumberText.ReadOnly = False
                    cboManufacturerId.Enabled = True
                    cboItemRegistrationStatus.Enabled = True
                    ManufacturerTextBox.ReadOnly = False
                    DeviceTypeText.ReadOnly = True
                    PurchasePriceText.ReadOnly = False
                    ItemDescText.ReadOnly = False
                    ModelText.ReadOnly = False
                    RegItemNameText.ReadOnly = False
                    PurchasedDateText_WRITE.ReadOnly = False
                    ImageButtonPurchasedDate.Enabled = True
                    'REQ-6002
                    RetailPriceText.ReadOnly = False
                    RegistrationDateText.ReadOnly = False
                    RegistrationDateImageButton.Enabled = True
                    IndixIDText.ReadOnly = False
                    ExpirationDateText.ReadOnly = True
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub InitializeFromFlowSession()
            State.inputParameters = CType(NavController.ParametersPassed, ReturnType)
        End Sub
        Protected Sub ValidateFields()
            If GetSelectedDescription(cboManufacturerId).ToUpper = "OTHER" Then
                If ManufacturerTextBox.Text.Trim() = String.Empty Then
                    ElitaPlusPage.SetLabelError(ManufacturerLabel)
                    Throw New GUIException(Message.ERR_SAVING_DATA, Assurant.ElitaPlus.Common.ErrorCodes.ERR_INVALID_MANUFACTURER)
                End If
            End If
            SetLabelColor(ManufacturerLabel)

            If String.IsNullOrEmpty(PurchasedDateText_WRITE.Text) Then
                ElitaPlusPage.SetLabelError(PurchasedDateLabel)
                Throw New GUIException(Message.ERR_SAVING_DATA, Assurant.ElitaPlus.Common.ErrorCodes.ERR_PURCHASED_DATE_IS_REQUIRED)
            Else
                If IsDate(GetDateString(PurchasedDateText_WRITE.Text)) = False Then
                    ElitaPlusPage.SetLabelError(PurchasedDateLabel)
                    Throw New GUIException(Message.ERR_SAVING_DATA, Assurant.ElitaPlus.Common.ErrorCodes.ERR_PURCHASED_DATE_IS_INVALID)
                End If
            End If
                SetLabelColor(PurchasedDateLabel)

            If String.IsNullOrEmpty(PurchasePriceText.Text) Then
                ElitaPlusPage.SetLabelError(PurchasePriceLabel)
                Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.ERR_PURCHASE_PRICE_IS_REQUIRED)
            Else
                If IsNumeric(PurchasePriceText.Text) = False Then
                    ElitaPlusPage.SetLabelError(PurchasePriceLabel)
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.ERR_PURCHASE_PRICE_IS_INVALID)
                End If
            End If
            SetLabelColor(PurchasePriceLabel)

            'REQ-6002
            If Not String.IsNullOrEmpty(RetailPriceText.Text) AndAlso IsNumeric(RetailPriceText.Text) = False Then
                ElitaPlusPage.SetLabelError(RetailPriceLabel)
                Throw New GUIException(Message.ERR_SAVING_DATA, Assurant.ElitaPlus.Common.ErrorCodes.ERR_RETAIL_PRICE_IS_INVALID)
            End If
            SetLabelColor(RetailPriceLabel)

            If Not String.IsNullOrEmpty(RegistrationDateText.Text) AndAlso IsDate(GetDateString(RegistrationDateText.Text)) = False Then
                ElitaPlusPage.SetLabelError(RegistrationDateLabel)
                Throw New GUIException(Message.ERR_SAVING_DATA, Assurant.ElitaPlus.Common.ErrorCodes.ERR_REGISTRATION_DATE_IS_INVALID)
            End If
            SetLabelColor(RegistrationDateLabel)
        End Sub
#End Region

#Region "Button Clicks"
        Private Sub btnApply_WRITE_Click1(sender As Object, e As System.EventArgs) Handles btnApply_WRITE.Click
            Try
                Dim ErrMsg As New Collections.Generic.List(Of String)
                ValidateFields()
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    If State.MyBO.DirtyColumns.Count = 1 Then
                        If (State.InitialMaufacturer = State.MyBO.Manufacturer) AndAlso (State.MyBO.DirtyColumns.ContainsKey("MANUFACTURER")) Then
                            Exit Sub
                        End If
                    End If

                    If State.MyBO.UpdateRegisterItem(ErrMsg, State.MyBO.Id) Then
                        State.HasDataChanged = True
                        PopulateFormFromBOs()
                        State.boChanged = True
                        State.isEdit = False
                        EnableDisableFields()
                        MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                    Else
                        MasterPage.MessageController.AddErrorAndShow(ErrMsg.ToArray, False)
                        State.isEdit = True
                        EnableDisableFields()
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                State.isEdit = True
                EnableDisableFields()
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click1(sender As Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                State.MyBO = New CertRegisteredItem(State.MyBO.Id)
                PopulateFormFromBOs()
                State.isEdit = False
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnBack_Click1(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Try
                moCertificate = State.MyBO.GetCertificate(State.certificateId)
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    If State.MyBO.DirtyColumns.Count = 1 Then
                        If (State.InitialMaufacturer = State.MyBO.Manufacturer) AndAlso (State.MyBO.DirtyColumns.ContainsKey("MANUFACTURER")) Then
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.boChanged)
                            NavController.Navigate(Me, "back", retObj)
                        End If
                    End If
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.boChanged)
                    NavController.Navigate(Me, "back", retObj)
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
                NavController.FlowSession(FlowSessionKeys.SESSION_CERT_REGISTERED_ITEM_ID) = State.CertRegisteredItemId
                NavController.Navigate(Me, "reg_item_history_selected")
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub cboManufacturerId_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboManufacturerId.SelectedIndexChanged
            If GetSelectedDescription(cboManufacturerId).ToUpper = "OTHER" Then
                ControlMgr.SetVisibleControl(Me, ManufacturerTextBox, True)
            Else
                ControlMgr.SetVisibleControl(Me, ManufacturerTextBox, False)
            End If
        End Sub

#End Region

#Region "Functions"
        Private Function GetDateString(strInput As String) As String
            Dim formatProvider = LocalizationMgr.CurrentFormatProvider

            Dim strOutput As String = Nothing

            Dim dtDate As Date

            If formatProvider.Name.Equals("ja-JP") Then
                Try
                    If IsDate(strInput) Then
                        strOutput = strInput
                    ElseIf Date.TryParseExact(strInput, {"MM/dd/yyyy", "M/dd/yyyy", "M/d/yyyy", "dd-MM-yyyy", "d-M-yyyy", "dd-M-yyyy", "MM/d/yyyy"}, Nothing, 0, dtDate) Then
                        strOutput = dtDate.ToString("MM/dd/yyyy")
                    End If

                Catch ex As Exception
                    If Date.TryParseExact(strInput, {"MM/dd/yyyy", "dd-MM-yyyy"}, Nothing, 0, dtDate) Then
                        strOutput = dtDate.ToString("MM/dd/yyyy")
                    End If
                End Try

            Else
                strOutput = strInput
            End If

            Return strOutput

        End Function


#End Region

    End Class

End Namespace
