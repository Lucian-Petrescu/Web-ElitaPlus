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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = New CommissionEntity(CType(Me.CallingParameters, Guid))
                If Me.State.MyBO.BankInfoId.Equals(Guid.Empty) Then
                    Me.State.MyBO.isBankInfoNeedDeletion = False
                Else
                    Me.State.MyBO.isBankInfoNeedDeletion = True
                End If
            Else
                Me.State.IsNew = True
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

#End Region


#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As CommissionEntity
        Public HasDataChanged As Boolean
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As CommissionEntity, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.UpdateBreadCrum()
        Me.MasterPage.MessageController.Clear()
        Try
            If Not Me.IsPostBack Then
                Me.MenuEnabled = False
                Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)

                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New CommissionEntity
                End If

                Dim CompanyGroupId As New CompanyGroup(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
                Me.State.MyBO.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                Me.State.use_comm_entity_type_id = CompanyGroupId.UseCommEntityTypeId

                If Me.State.IsNew = True Then
                    CreateNew()
                End If

                Me.moBankInfoController.Visible = False
                Me.moBankInfoController.ReAssignTabIndex(BankInfoStartIndex)
                Me.moAddressController.ReAssignTabIndex(AddressInfoStartIndex)
                Me.State.BankInfoBO = Nothing
                PopulateDropdowns()
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Me.IsPostBack Then
                If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, Me.GetSelectedItem(Me.cboPaymentMethodId)) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                    If (Not Me.State.BankInfoBO Is Nothing) Then
                        Me.State.BankInfoBO.SourceCountryID = Me.AddressCtr.GetCountryValue
                    End If
                End If
            Else
                Me.AddLabelDecorations(Me.State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub
#End Region
    Private Sub UpdateBreadCrum()
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("COMMISSION_ENTITY")
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("COMMISSION_ENTITY")
        Me.MasterPage.MessageController.Clear()
        Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("TABLES") & ElitaBase.Sperator & Me.MasterPage.PageTab
    End Sub


#Region "Controlling Logic"

    Protected Sub EnableDisableFields()
        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)

        If Me.State.MyBO.IsNew Then
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
        If Me.State.use_comm_entity_type_id.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
            ControlMgr.SetVisibleControl(Me, Me.lblTaxid, True)
            ControlMgr.SetVisibleControl(Me, Me.txtTaxid, True)
            ControlMgr.SetVisibleControl(Me, Me.lblCommissionEntityTypeId, True)
            ControlMgr.SetVisibleControl(Me, Me.cboCommissionEntityTypeId, True)
        Else
            ControlMgr.SetVisibleControl(Me, Me.lblTaxid, False)
            ControlMgr.SetVisibleControl(Me, Me.txtTaxid, False)
            ControlMgr.SetVisibleControl(Me, Me.lblCommissionEntityTypeId, False)
            ControlMgr.SetVisibleControl(Me, Me.cboCommissionEntityTypeId, False)
        End If

        AddressCtr.EnableControls(False, True)

    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, PAYMENTMETHOD_PROPERTY, Me.lblPaymentMethod)
        Me.BindBOPropertyToLabel(Me.State.MyBO, ENTITYNAME_PROPERTY, Me.lblEntityName)
        Me.BindBOPropertyToLabel(Me.State.MyBO, EMAIL_PROPERTY, Me.lblEmail)
        Me.BindBOPropertyToLabel(Me.State.MyBO, PHONE_PROPERTY, Me.lblPhone)
        Me.BindBOPropertyToLabel(Me.State.MyBO, DISPLAY_PROPERTY, Me.lblDisplay)
        Me.BindBOPropertyToLabel(Me.State.MyBO, TAX_ID, Me.lblTaxid)
        Me.BindBOPropertyToLabel(Me.State.MyBO, COMMISSION_ENTITY_TYPE_ID, Me.lblCommissionEntityTypeId)

        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub
    Protected Sub PopulateDropdowns()
        Dim i As Integer
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        With Me.State.MyBO

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
        If Me.State.IsNew = True Then
            cboDisplayId.Items.FindByText(oYes_String).Selected = True
        End If
    End Sub
    Protected Sub PopulateFormFromBOs()
        With Me.State.MyBO

            Me.PopulateControlFromBOProperty(Me.txtEntityName, .EntityName)
            Me.PopulateControlFromBOProperty(Me.txtPhone, .Phone)
            Me.PopulateControlFromBOProperty(Me.txtEmail, .Email)
            Me.PopulateControlFromBOProperty(Me.txtTaxid, .TaxId)

            If Not Me.State.IsNew Then
                Me.SetSelectedItem(Me.cboDisplayId, .DisplayId)
                Me.SetSelectedItem(Me.cboPaymentMethodId, .PaymentMethodId)
                Me.SetSelectedItem(Me.cboCommissionEntityTypeId, .CommissionEntityTypeid)
            End If

            AddressCtr.Bind(Me.State.MyBO)

            If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, Me.State.MyBO.PaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                Me.moBankInfoController.Visible = True
                Me.State.BankInfoBO = Me.State.MyBO.Add_BankInfo
                Me.UserBankInfoCtr.SetTheRequiredFields()
                Me.UserBankInfoCtr.Bind(Me.State.BankInfoBO, Me.MasterPage.MessageController)
                Me.State.BankInfoBO.SourceCountryID = Me.AddressCtr.GetCountryValue
            Else
                Me.State.BankInfoBO = Nothing
                Me.moBankInfoController.Visible = False
            End If


            If Me.State.MyBO.BankInfoId.Equals(Guid.Empty) Then
                Me.State.MyBO.IsNewBankInfo = True
            Else
                Me.State.MyBO.IsNewBankInfo = False
            End If

        End With
        If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, Me.State.MyBO.PaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
            pnlLine.Visible = False
        Else
            pnlLine.Visible = True
        End If
    End Sub

    Protected Sub PopulateBOsFromForm()

        With Me.State.MyBO

            Me.PopulateBOProperty(Me.State.MyBO, "EntityName", Me.txtEntityName)
            Me.PopulateBOProperty(Me.State.MyBO, "Phone", Me.txtPhone)
            Me.PopulateBOProperty(Me.State.MyBO, "Email", Me.txtEmail)
            Me.PopulateBOProperty(Me.State.MyBO, "PaymentMethodId", Me.cboPaymentMethodId)
            Me.PopulateBOProperty(Me.State.MyBO, "DisplayId", Me.cboDisplayId)
            Me.PopulateBOProperty(Me.State.MyBO, "TaxId", Me.txtTaxid)
            Me.PopulateBOProperty(Me.State.MyBO, "CommissionEntityTypeid", Me.cboCommissionEntityTypeId)

            Me.State.MyBO.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Me.AddressCtr.PopulateBOFromControl(True)
            PopulateBankBOFromForm()
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub
    Sub PopulateBankBOFromForm()
        If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, Me.State.MyBO.PaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
            Me.State.MyBO.BankInfoId = Me.State.BankInfoBO.Id
            Me.UserBankInfoCtr.PopulateBOFromControl()
        Else
            Me.State.BankInfoBO = Nothing
            Me.State.MyBO.BankInfoId = Nothing
        End If
    End Sub

    Protected Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        Me.State.MyBO = New CommissionEntity
        Me.State.IsNew = True
        PopulateDropdowns()
        Me.PopulateFormFromBOs()
        Me.cboPaymentMethodId.SelectedIndex = -1
        Me.EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        Me.PopulateBOsFromForm()

        Dim newObj As New CommissionEntity
        newObj.Copy(Me.State.MyBO)
        If Not newObj.BankInfoId.Equals(Guid.Empty) Then
            ' copy the original bankinfo
            newObj.BankInfoId = Guid.Empty
            newObj.Add_BankInfo()
            newObj.BankInfoId = newObj.CurrentBankInfo.Id
            newObj.CurrentBankInfo.CopyFrom(Me.State.MyBO.CurrentBankInfo)
            Me.State.BankInfoBO = newObj.CurrentBankInfo
            Me.UserBankInfoCtr.Bind(Me.State.BankInfoBO, Me.MasterPage.MessageController)
        End If

        Me.State.MyBO = newObj
        Me.State.MyBO.EntityName = Nothing
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
        'Me.State.MyBO.IsNewWithCopy = True
        'create the backup copy
        Me.State.ScreenSnapShotBO = New CommissionEntity
        Me.State.ScreenSnapShotBO.Copy(Me.State.MyBO)

        'Me.PopulateBOsFromForm()
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_OK Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                Me.State.MyBO.Save()
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_CANCEL Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Private Sub cboPaymentMethodId_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPaymentMethodId.SelectedIndexChanged
        Try
            'Me.PopulateBOProperty(Me.State.MyBO, "PaymentMethodId", Me.cboPaymentMethodId)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, Me.GetSelectedItem(Me.cboPaymentMethodId)) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                ' SHOW THE BANK INFO USER CONTROL HERE -----
                Me.moBankInfoController.Visible = True
                Me.State.BankInfoBO = Nothing
                Me.State.BankInfoBO = Me.State.MyBO.Add_BankInfo
                Me.UserBankInfoCtr.SetTheRequiredFields()
                Me.UserBankInfoCtr.Bind(Me.State.BankInfoBO, Me.MasterPage.MessageController)
                If Not (Me.AddressCtr.GetCountryValue.Equals(Guid.Empty)) Then
                    Me.UserBankInfoCtr.SetCountryValue(Me.AddressCtr.GetCountryValue)
                    Me.State.BankInfoBO.SourceCountryID = Me.AddressCtr.GetCountryValue
                End If
                pnlLine.Visible = False
            Else
                Me.moBankInfoController.Visible = False
                Me.State.BankInfoBO = Nothing
                pnlLine.Visible = True
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            If Me.State.MyBO.ConstrVoilation = False Then
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
            Else
                Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        End Try
    End Sub
    Private Sub btnCopy_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            Me.PopulateBOsFromForm()
            If (Me.State.MyBO.IsDirty) Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                Me.CreateNewWithCopy()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnDelete_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            Me.State.MyBO.IsDelete = True
            Me.State.MyBO.DeleteAndSave()
            Me.State.HasDataChanged = True
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.PopulateBOsFromForm()
            If (Me.State.MyBO.IsDirty) Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.CreateNew()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnApply_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click
        Try
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                Me.State.MyBO.Save()
                Me.State.HasDataChanged = True
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
            Else
                Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New CommissionEntity(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                CreateNew()
            End If
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region
End Class
