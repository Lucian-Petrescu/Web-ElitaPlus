Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms


Partial Class CancellationReasonForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents CodeValidator As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl


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
    Public Const URL As String = "CancellationReasonForm.aspx"
    Private Const ONE_ITEM As Integer = 1
#End Region

#Region "Properties"
    Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl_New
        Get
            If CompanyDropControl Is Nothing Then
                CompanyDropControl = CType(FindControl("CompanyDropControl"), MultipleColumnDDLabelControl_New)
            End If
            Return CompanyDropControl
        End Get
    End Property
#End Region

#Region "Page Return Type"
    ' the information here is used in the search page
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As CancellationReason
        Public BoChanged As Boolean = False
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As CancellationReason, Optional ByVal boChanged As Boolean = False)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.BoChanged = boChanged
        End Sub
    End Class
#End Region

#Region "Page State"


    Class MyState
        Public MyBO As CancellationReason
        Public ScreenSnapShotBO As CancellationReason
        Public cancellationReasonChanged As Boolean = False
        Public IsCancellationReasonNew As Boolean = False
        Public CompanyId As Guid

        Public actionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
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
                State.MyBO = New CancellationReason(CType(CallingParameters, Guid))
                State.cancellationReasonChanged = False
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        MasterPage.MessageController.Clear_Hide()
        Try
            If Not IsPostBack Then

                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                UpdateBreadCrum()
                MenuEnabled = False
                AddConfirmation(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                If State.MyBO Is Nothing Then
                    State.MyBO = New CancellationReason
                    State.cancellationReasonChanged = False
                End If
                SetButtonsState(State.MyBO.IsNew)
                PopulateAll()
                If GetSelectedItem(cboRefundDestination).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_REFUND_DESTINATION, Codes.REFUND_DESTINATION__CUSTOMER)) Then
                    ControlMgr.SetEnableControl(Me, labelDefRefPaymentMethod, True)
                    ControlMgr.SetEnableControl(Me, cboDefRefPaymentMethod, True)
                End If
            End If
                BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub
#End Region

#Region "Controlling Logic"

    Private Sub PopulateCompanyDropDowns()
        UserCompanyMultipleDrop.SetControl(False, UserCompanyMultipleDrop.MODES.NEW_MODE, True, Nothing, , True)
        If State.MyBO IsNot Nothing Then
            UserCompanyMultipleDrop.SelectedGuid = State.MyBO.CompanyId
        End If
    End Sub

    Protected Sub EnableDisableFields()

        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)

        'New With Copy Button
        If State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        End If
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, "Code", lblCancCode)
        BindBOPropertyToLabel(State.MyBO, "Description", lblCancDesc)
        BindBOPropertyToLabel(State.MyBO, "RefundComputeMethodId", labelRefundComputeMethod)
        BindBOPropertyToLabel(State.MyBO, "RefundDestinationId", labelRefundDestination)
        BindBOPropertyToLabel(State.MyBO, "InputAmtReqId", labelInputAmtReq)
        BindBOPropertyToLabel(State.MyBO, "DisplayCodeId", labelDisplayCode)
        BindBOPropertyToLabel(State.MyBO, "DefRefundPaymentMethodId", labelDefRefPaymentMethod)
        BindBOPropertyToLabel(State.MyBO, "IsLawful", labelIsLawful)
        BindBOPropertyToLabel(State.MyBO, "BenefitCancelReasonCode", lblBenefitCancelCode)
        ClearGridHeadersAndLabelsErrSign()
    End Sub


    Protected Sub PopulateFormFromBOs()
        With State.MyBO
            PopulateControlFromBOProperty(TextboxCode, .Code)
            PopulateControlFromBOProperty(TextboxDescription, .Description)

            SetSelectedItem(cboRefundComputeMethod, .RefundComputeMethodId)
            SetSelectedItem(cboRefundDestination, .RefundDestinationId)
            SetSelectedItem(cboInputAmtReq, .InputAmtReqId)
            SetSelectedItem(cboDisplayCode, .DisplayCodeId)
            SetSelectedItem(cboDefRefPaymentMethod, .DefRefundPaymentMethodId)
            If .IsLawful IsNot Nothing Then
                SetSelectedItem(cboIsLawful, .IsLawful)
            Else
                SetSelectedItem(cboIsLawful, "YESNO-N")
            End If
            PopulateControlFromBOProperty(txtBenefitCancelCode, .BenefitCancelReasonCode)
        End With

    End Sub

    Protected Sub PopulateBOsFormFrom()
        With State.MyBO
            PopulateBOProperty(State.MyBO, "Code", TextboxCode)
            PopulateBOProperty(State.MyBO, "Description", TextboxDescription)
            PopulateBOProperty(State.MyBO, "RefundComputeMethodId", cboRefundComputeMethod)
            PopulateBOProperty(State.MyBO, "RefundDestinationId", cboRefundDestination)
            PopulateBOProperty(State.MyBO, "InputAmtReqId", cboInputAmtReq)
            PopulateBOProperty(State.MyBO, "DisplayCodeId", cboDisplayCode)
            PopulateBOProperty(State.MyBO, "DefRefundPaymentMethodId", cboDefRefPaymentMethod)
            PopulateBOProperty(State.MyBO, "IsLawful", cboIsLawful, False, True)
            PopulateBOProperty(State.MyBO, "BenefitCancelReasonCode", txtBenefitCancelCode)

            If State.MyBO.IsNew AndAlso ElitaPlusIdentity.Current.ActiveUser.Companies.Count > 1 Then
                State.MyBO.CompanyId = UserCompanyMultipleDrop.SelectedGuid
            End If
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub


    Protected Sub CreateNew()

        State.ScreenSnapShotBO = Nothing
        State.MyBO = New CancellationReason
        ClearAll()
        State.cancellationReasonChanged = False
        SetButtonsState(State.MyBO.IsNew)
        PopulateAll()
        'Me.PopulateFormFromBOs()
        'Me.EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        PopulateBOsFormFrom()
        'Me.State.MyBO = New CancellationReason

        Dim newObj As New CancellationReason
        newObj.Copy(State.MyBO)

        State.MyBO = newObj

        'Me.State.MyBO.Id = Guid.Empty
        'Me.State.MyBO.IsNew = True

        With State.MyBO '(newObj)
            .Code = Nothing
        End With

        ' Me.EnableDisableFields()
        SetButtonsState(State.MyBO.IsNew)
        'create the backup copy for undo
        State.ScreenSnapShotBO = New CancellationReason
        State.ScreenSnapShotBO.Clone(State.MyBO)
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

        'If Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_OK Then
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            Select Case State.actionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    State.MyBO.Save()
                    State.cancellationReasonChanged = True
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.cancellationReasonChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    State.MyBO.Save()
                    State.cancellationReasonChanged = True
                    AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    State.MyBO.Save()
                    State.cancellationReasonChanged = True
                    AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    CreateNewWithCopy()
            End Select
            'ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_CANCEL Then
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.actionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.cancellationReasonChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    CreateNewWithCopy()
            End Select
        End If

        'Clean after consuming the action
        State.actionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub

#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.actionInProgress = ElitaPlusPage.DetailPageCommand.Back

                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM,
                                               HiddenSaveChangesPromptResponse)
                State.actionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.cancellationReasonChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                If State.MyBO.IsNew Then
                    State.MyBO.CompanyId = UserCompanyMultipleDrop.SelectedGuid
                End If
                State.MyBO.Save()
                State.cancellationReasonChanged = False
                PopulateAll()
                EnableDisableFields()
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New CancellationReason(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                State.MyBO = New CancellationReason
            End If
            PopulateAll()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            State.MyBO.Delete()
            State.MyBO.Save()
            State.cancellationReasonChanged = True
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.cancellationReasonChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            State.MyBO.RejectChanges()
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            'Me.PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.actionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


    Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                State.actionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewWithCopy()

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


#End Region

#Region "Gui-Validation"

    Private Sub SetButtonsState(bIsNew As Boolean)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
        'ControlMgr.SetEnableControl(Me, moDealerDrop, bIsNew)
        ControlMgr.SetEnableControl(Me, TextboxCode, bIsNew)
        ControlMgr.SetEnableControl(Me, labelDefRefPaymentMethod, bIsNew)
        ControlMgr.SetEnableControl(Me, cboDefRefPaymentMethod, bIsNew)

    End Sub

#End Region

#Region "Regions: Attach - Detach Event Handlers"

    Private Sub UserControlAvailableExcludeRoles_Attach(aSrc As Generic.UserControlAvailableSelected_New, attachedList As System.Collections.ArrayList) Handles UserControlAvailableExcludeRoles.Attach
        Try
            If attachedList.Count > 0 Then
                State.MyBO.AttachRoles(attachedList)
                'Me.PopulateDetailMfgGrid()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UserControlAvailableExcludeRoles_Detach(aSrc As Generic.UserControlAvailableSelected_New, detachedList As System.Collections.ArrayList) Handles UserControlAvailableExcludeRoles.Detach
        Try
            If detachedList.Count > 0 Then
                State.MyBO.DetachRoles(detachedList)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Populate"


    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("CancellationReason_List")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CancellationReason_List")
            End If
        End If
    End Sub
    Sub PopulateUserControlAvailableSelectedRoles()
        UserControlAvailableExcludeRoles.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableExcludeRoles, False)
        Dim oDealer As Dealer
        Dim CountryId As Guid

        With State.MyBO
            If Not .Id.Equals(Guid.Empty) Then

                Dim availableDv As DataView = .GetAvailableRoles
                Dim selectedDv As DataView = .GetSelectedRoles
                UserControlAvailableExcludeRoles.SetAvailableData(availableDv, LookupListNew.COL_CODE_AND_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                UserControlAvailableExcludeRoles.SetSelectedData(selectedDv, LookupListNew.COL_CODE_AND_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                ControlMgr.SetVisibleControl(Me, UserControlAvailableExcludeRoles, True)

            End If
        End With

    End Sub

    Protected Sub PopulateDropdowns()
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", langId, True)
        Dim yesNoLkL As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
        ' Me.BindListControlToDataView(Me.cboInputAmtReq, yesNoLkL)
        cboInputAmtReq.Populate(yesNoLkL, New PopulateOptions() With
           {
              .AddBlankItem = True
           })
        'Me.BindListControlToDataView(Me.cboDisplayCode, yesNoLkL)
        cboDisplayCode.Populate(yesNoLkL, New PopulateOptions() With
           {
              .AddBlankItem = True
           })
        ' Me.BindListControlToDataView(Me.cboRefundComputeMethod, LookupListNew.GetRefundComputeMethodLookupList(langId, True)) 'RMETH
        cboRefundComputeMethod.Populate(CommonConfigManager.Current.ListManager.GetList("RMETH", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
           {
              .AddBlankItem = True
           })
        '  Me.BindListControlToDataView(Me.cboRefundDestination, LookupListNew.GetRefundDestinationLookupList(langId, True)) 'REFDS
        cboRefundDestination.Populate(CommonConfigManager.Current.ListManager.GetList("REFDS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
           {
              .AddBlankItem = True
           })
        'Me.cboIsLawful.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
        cboIsLawful.Populate(yesNoLkL, New PopulateOptions() With
        {
           .ValueFunc = AddressOf .GetExtendedCode
        })
        PopulatePaymentMethodDropDown()
        PopulateUserControlAvailableSelectedRoles()
    End Sub
    Protected Sub PopulatePaymentMethodDropDown()
        ' Dim condition As String = "CODE not in ('CTT','PYO')" 'Remove Bank Transfer and Payment Order'PaymentMethodByRoleCompany
        ' Dim PaymentMethodDV As DataView = LookupListNew.GetPaymentMethodLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, , (ElitaPlusIdentity.Current.ActiveUser.Id).ToString, Me.State.MyBO.CompanyId.ToString, True)
        'PaymentMethodDV.RowFilter = condition
        'Me.BindListControlToDataView(Me.cboDefRefPaymentMethod, PaymentMethodDV, "DESCRIPTION", "ID", True)
        Dim listcontext As ListContext = New ListContext()
        listcontext.CompanyId = State.MyBO.CompanyId
        listcontext.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        listcontext.UserId = ElitaPlusIdentity.Current.ActiveUser.Id
        Dim paymentLKL As ListItem() = CommonConfigManager.Current.ListManager.GetList("PaymentMethodByRoleCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
        Dim filteredList As ListItem() = (From x In paymentLKL
                                          Where Not x.Code = "CTT" OrElse Not x.Code = "PYO"
                                          Select x).ToArray()
        cboDefRefPaymentMethod.Populate(filteredList, New PopulateOptions() With
             {
            .AddBlankItem = True
             })
    End Sub

    Private Sub PopulateAll()
        If State.cancellationReasonChanged = True Then
            PopulateDropdowns()
            PopulateCompanyDropDowns()
            PopulateUserControlAvailableSelectedRoles()
        Else
            ClearAll()
            PopulateCompanyDropDowns()
            PopulateDropdowns()
            PopulateFormFromBOs()
            PopulateUserControlAvailableSelectedRoles()
        End If
    End Sub

#End Region

#Region "Clear"

    Private Sub ClearTexts()
        TextboxCode.Text = Nothing
        TextboxDescription.Text = Nothing
        txtBenefitCancelCode.text = Nothing
    End Sub

    Private Sub ClearAll()
        TextboxCode.Text = Nothing
        TextboxDescription.Text = Nothing
        UserCompanyMultipleDrop.ClearMultipleDrop()
        ClearList(cboRefundComputeMethod)
        ClearList(cboRefundDestination)
        ClearList(cboInputAmtReq)
        ClearList(cboDisplayCode)
        ClearList(cboDefRefPaymentMethod)
        ClearList(cboIsLawful)
        txtBenefitCancelCode.text = Nothing
        'Me.State.ProductPolicySearchDV = Nothing
        'ClearList(moUpfrontCommId)
    End Sub

    Private Sub cboRefundDestination_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboRefundDestination.SelectedIndexChanged
        Try

            If GetSelectedItem(cboRefundDestination).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_REFUND_DESTINATION, Codes.REFUND_DESTINATION__CUSTOMER)) Then
                ControlMgr.SetEnableControl(Me, labelDefRefPaymentMethod, True)
                ControlMgr.SetEnableControl(Me, cboDefRefPaymentMethod, True)

                PopulatePaymentMethodDropDown()
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region


End Class
