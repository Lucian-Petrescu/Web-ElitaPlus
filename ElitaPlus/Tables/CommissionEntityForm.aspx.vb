Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Partial Class CommissionEntityForm
    Inherits ElitaPlusPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    Protected WithEvents moAddressController As UserControlAddress_New
    Protected WithEvents moBankInfoController As UserControlBankInfo
    Public oYes_String As String
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
    Public Shared URL As String = "CommissionEntityForm.aspx"
    Public Const PAYMENTMETHOD_PROPERTY As String = "PaymentMethodId"
    Public Const ENTITYNAME_PROPERTY As String = "EntityName"
    Public Const EMAIL_PROPERTY As String = "Email"
    Public Const PHONE_PROPERTY As String = "Phone"
    Public Const DISPLAY_PROPERTY As String = "DisplayId"
    Private Const COMMISSIONENTITY_LIST_FORM001 As String = "COMMISSIONENTITY_LIST_FORM001" ' Maintain commission entity List Exception
    Public Const BankInfoStartIndex As Int16 = 11
    Public Const AddressInfoStartIndex As Int16 = 4
    Public Const TAX_ID As String = "Taxid"
    Public Const COMMISSION_ENTITY_TYPE_ID As String = "CommissionEntityTypeid"
    Public Const YES As String = "Y"
#End Region

#Region "Properties"

    Public ReadOnly Property AddressCtr() As UserControlAddress_New
        Get
            Return moAddressController
        End Get
    End Property

    Public ReadOnly Property UserBankInfoCtr() As UserControlBankInfo
        Get
            If moBankInfoController Is Nothing Then
                moBankInfoController = CType(FindControl("moBankInfoController"), UserControlBankInfo)
            End If
            Return moBankInfoController
        End Get
    End Property
#End Region

#Region "Page State"

    Class MyState
        Public MyBO As CommissionEntity
        Public ScreenSnapShotBO As CommissionEntity
        Public BankInfoBO As BankInfo

        Public IsNew As Boolean
        Public IsACopy As Boolean
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        Public use_comm_entity_type_id As Guid = Guid.Empty
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
                State.MyBO = New CommissionEntity(CType(CallingParameters, Guid))
                If State.MyBO.BankInfoId.Equals(Guid.Empty) Then
                    State.MyBO.isBankInfoNeedDeletion = False
                Else
                    State.MyBO.isBankInfoNeedDeletion = True
                End If
            Else
                State.IsNew = True
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

#End Region


#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As CommissionEntity
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As CommissionEntity, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        UpdateBreadCrum()
        MasterPage.MessageController.Clear()
        Try
            If Not IsPostBack Then
                MenuEnabled = False
                AddConfirmation(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)

                If State.MyBO Is Nothing Then
                    State.MyBO = New CommissionEntity
                End If

                Dim CompanyGroupId As New CompanyGroup(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
                State.MyBO.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                State.use_comm_entity_type_id = CompanyGroupId.UseCommEntityTypeId

                If State.IsNew = True Then
                    CreateNew()
                End If

                moBankInfoController.Visible = False
                moBankInfoController.ReAssignTabIndex(BankInfoStartIndex)
                moAddressController.ReAssignTabIndex(AddressInfoStartIndex)
                State.BankInfoBO = Nothing
                PopulateDropdowns()
                PopulateFormFromBOs()
                EnableDisableFields()
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If IsPostBack Then
                If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, GetSelectedItem(cboPaymentMethodId)) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                    If (State.BankInfoBO IsNot Nothing) Then
                        State.BankInfoBO.SourceCountryID = AddressCtr.GetCountryValue
                    End If
                End If
            Else
                AddLabelDecorations(State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub
#End Region
    Private Sub UpdateBreadCrum()
        MasterPage.UsePageTabTitleInBreadCrum = False
        MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("COMMISSION_ENTITY")
        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("COMMISSION_ENTITY")
        MasterPage.MessageController.Clear()
        MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("TABLES") & ElitaBase.Sperator & MasterPage.PageTab
    End Sub


#Region "Controlling Logic"

    Protected Sub EnableDisableFields()
        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)

        If State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        End If

        If (ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__IHQ_SUPPORT)) Then
            lblDisplay.Visible = True
            cboDisplayId.Visible = True
        Else
            lblDisplay.Visible = False
            cboDisplayId.Visible = False
        End If
        If State.use_comm_entity_type_id.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
            ControlMgr.SetVisibleControl(Me, lblTaxid, True)
            ControlMgr.SetVisibleControl(Me, txtTaxid, True)
            ControlMgr.SetVisibleControl(Me, lblCommissionEntityTypeId, True)
            ControlMgr.SetVisibleControl(Me, cboCommissionEntityTypeId, True)
        Else
            ControlMgr.SetVisibleControl(Me, lblTaxid, False)
            ControlMgr.SetVisibleControl(Me, txtTaxid, False)
            ControlMgr.SetVisibleControl(Me, lblCommissionEntityTypeId, False)
            ControlMgr.SetVisibleControl(Me, cboCommissionEntityTypeId, False)
        End If

        AddressCtr.EnableControls(False, True)

    End Sub

    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, PAYMENTMETHOD_PROPERTY, lblPaymentMethod)
        BindBOPropertyToLabel(State.MyBO, ENTITYNAME_PROPERTY, lblEntityName)
        BindBOPropertyToLabel(State.MyBO, EMAIL_PROPERTY, lblEmail)
        BindBOPropertyToLabel(State.MyBO, PHONE_PROPERTY, lblPhone)
        BindBOPropertyToLabel(State.MyBO, DISPLAY_PROPERTY, lblDisplay)
        BindBOPropertyToLabel(State.MyBO, TAX_ID, lblTaxid)
        BindBOPropertyToLabel(State.MyBO, COMMISSION_ENTITY_TYPE_ID, lblCommissionEntityTypeId)

        ClearGridHeadersAndLabelsErrSign()
    End Sub
    Protected Sub PopulateDropdowns()
        Dim i As Integer
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        With State.MyBO

            'Me.BindListControlToDataView(Me.cboPaymentMethodId, LookupListNew.GetPaymentMethodLookupList(langId), , , True)
            Dim paymentMethod As ListItem() = CommonConfigManager.Current.ListManager.GetList("PMTHD", Thread.CurrentPrincipal.GetLanguageCode())
            cboPaymentMethodId.Populate(paymentMethod, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                   })

            Dim commEntityType As ListItem() = CommonConfigManager.Current.ListManager.GetList("CET", Thread.CurrentPrincipal.GetLanguageCode())
            cboCommissionEntityTypeId.Populate(commEntityType, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                   })
        End With

        'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", langId, True)
        'Me.BindListControlToDataView(Me.cboDisplayId, yesNoLkL)
        Dim yesNoLkL As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
        cboDisplayId.Populate(yesNoLkL, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                   })
        'For i = 0 To yesNoLkL.Count - 1
        '    If CType(yesNoLkL(i)("Code"), String) = YES Then
        '        ' oYes_Guid = New Guid(CType(yesNoLkL(i)("ID"), Byte()))
        '        oYes_String = CType(yesNoLkL(i)("Description"), String)
        '        Exit For
        '    End If
        'Next
        For i = 0 To yesNoLkL.Count - 1
            If yesNoLkL(i).Code = YES Then
                oYes_String = yesNoLkL(i).Translation
                Exit For
            End If
        Next
        If State.IsNew = True Then
            cboDisplayId.Items.FindByText(oYes_String).Selected = True
        End If
    End Sub
    Protected Sub PopulateFormFromBOs()
        With State.MyBO

            PopulateControlFromBOProperty(txtEntityName, .EntityName)
            PopulateControlFromBOProperty(txtPhone, .Phone)
            PopulateControlFromBOProperty(txtEmail, .Email)
            PopulateControlFromBOProperty(txtTaxid, .TaxId)

            If Not State.IsNew Then
                SetSelectedItem(cboDisplayId, .DisplayId)
                SetSelectedItem(cboPaymentMethodId, .PaymentMethodId)
                SetSelectedItem(cboCommissionEntityTypeId, .CommissionEntityTypeid)
            End If

            AddressCtr.Bind(State.MyBO)

            If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, State.MyBO.PaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                moBankInfoController.Visible = True
                State.BankInfoBO = State.MyBO.Add_BankInfo
                UserBankInfoCtr.SetTheRequiredFields()
                UserBankInfoCtr.Bind(State.BankInfoBO, MasterPage.MessageController)
                State.BankInfoBO.SourceCountryID = AddressCtr.GetCountryValue
            Else
                State.BankInfoBO = Nothing
                moBankInfoController.Visible = False
            End If


            If State.MyBO.BankInfoId.Equals(Guid.Empty) Then
                State.MyBO.IsNewBankInfo = True
            Else
                State.MyBO.IsNewBankInfo = False
            End If

        End With
        If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, State.MyBO.PaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
            pnlLine.Visible = False
        Else
            pnlLine.Visible = True
        End If
    End Sub

    Protected Sub PopulateBOsFromForm()

        With State.MyBO

            PopulateBOProperty(State.MyBO, "EntityName", txtEntityName)
            PopulateBOProperty(State.MyBO, "Phone", txtPhone)
            PopulateBOProperty(State.MyBO, "Email", txtEmail)
            PopulateBOProperty(State.MyBO, "PaymentMethodId", cboPaymentMethodId)
            PopulateBOProperty(State.MyBO, "DisplayId", cboDisplayId)
            PopulateBOProperty(State.MyBO, "TaxId", txtTaxid)
            PopulateBOProperty(State.MyBO, "CommissionEntityTypeid", cboCommissionEntityTypeId)

            State.MyBO.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            AddressCtr.PopulateBOFromControl(True)
            PopulateBankBOFromForm()
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub
    Sub PopulateBankBOFromForm()
        If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, State.MyBO.PaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
            State.MyBO.BankInfoId = State.BankInfoBO.Id
            UserBankInfoCtr.PopulateBOFromControl()
        Else
            State.BankInfoBO = Nothing
            State.MyBO.BankInfoId = Nothing
        End If
    End Sub

    Protected Sub CreateNew()
        State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        State.MyBO = New CommissionEntity
        State.IsNew = True
        PopulateDropdowns()
        PopulateFormFromBOs()
        cboPaymentMethodId.SelectedIndex = -1
        EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        PopulateBOsFromForm()

        Dim newObj As New CommissionEntity
        newObj.Copy(State.MyBO)
        If Not newObj.BankInfoId.Equals(Guid.Empty) Then
            ' copy the original bankinfo
            newObj.BankInfoId = Guid.Empty
            newObj.Add_BankInfo()
            newObj.BankInfoId = newObj.CurrentBankInfo.Id
            newObj.CurrentBankInfo.CopyFrom(State.MyBO.CurrentBankInfo)
            State.BankInfoBO = newObj.CurrentBankInfo
            UserBankInfoCtr.Bind(State.BankInfoBO, MasterPage.MessageController)
        End If

        State.MyBO = newObj
        State.MyBO.EntityName = Nothing
        PopulateFormFromBOs()
        EnableDisableFields()
        'Me.State.MyBO.IsNewWithCopy = True
        'create the backup copy
        State.ScreenSnapShotBO = New CommissionEntity
        State.ScreenSnapShotBO.Copy(State.MyBO)

        'Me.PopulateBOsFromForm()
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = CONFIRM_MESSAGE_OK Then
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
        ElseIf confResponse IsNot Nothing AndAlso confResponse = CONFIRM_MESSAGE_CANCEL Then
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
    End Sub

    Private Sub cboPaymentMethodId_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboPaymentMethodId.SelectedIndexChanged
        Try
            'Me.PopulateBOProperty(Me.State.MyBO, "PaymentMethodId", Me.cboPaymentMethodId)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, GetSelectedItem(cboPaymentMethodId)) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                ' SHOW THE BANK INFO USER CONTROL HERE -----
                moBankInfoController.Visible = True
                State.BankInfoBO = Nothing
                State.BankInfoBO = State.MyBO.Add_BankInfo
                UserBankInfoCtr.SetTheRequiredFields()
                UserBankInfoCtr.Bind(State.BankInfoBO, MasterPage.MessageController)
                If Not (AddressCtr.GetCountryValue.Equals(Guid.Empty)) Then
                    UserBankInfoCtr.SetCountryValue(AddressCtr.GetCountryValue)
                    State.BankInfoBO.SourceCountryID = AddressCtr.GetCountryValue
                End If
                pnlLine.Visible = False
            Else
                moBankInfoController.Visible = False
                State.BankInfoBO = Nothing
                pnlLine.Visible = True
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            If State.MyBO.ConstrVoilation = False Then
                HandleErrors(ex, MasterPage.MessageController)
                AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                State.LastErrMsg = MasterPage.MessageController.Text
            Else
                ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
            End If
        End Try
    End Sub
    Private Sub btnCopy_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            PopulateBOsFromForm()
            If (State.MyBO.IsDirty) Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewWithCopy()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnDelete_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            State.MyBO.IsDelete = True
            State.MyBO.DeleteAndSave()
            State.HasDataChanged = True
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub btnNew_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            PopulateBOsFromForm()
            If (State.MyBO.IsDirty) Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnApply_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnApply_WRITE.Click
        Try
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                State.MyBO.Save()
                State.HasDataChanged = True
                PopulateFormFromBOs()
                EnableDisableFields()
                AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
            Else
                AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New CommissionEntity(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                CreateNew()
            End If
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
End Class
