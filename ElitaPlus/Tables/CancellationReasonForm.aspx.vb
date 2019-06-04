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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As CancellationReason, Optional ByVal boChanged As Boolean = False)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = New CancellationReason(CType(Me.CallingParameters, Guid))
                Me.State.cancellationReasonChanged = False
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.MasterPage.MessageController.Clear_Hide()
        Try
            If Not Me.IsPostBack Then

                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                UpdateBreadCrum()
                Me.MenuEnabled = False
                Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New CancellationReason
                    Me.State.cancellationReasonChanged = False
                End If
                SetButtonsState(Me.State.MyBO.IsNew)
                PopulateAll()
                If GetSelectedItem(Me.cboRefundDestination).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_REFUND_DESTINATION, Codes.REFUND_DESTINATION__CUSTOMER)) Then
                    ControlMgr.SetEnableControl(Me, labelDefRefPaymentMethod, True)
                    ControlMgr.SetEnableControl(Me, cboDefRefPaymentMethod, True)
                End If
            End If
                BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub
#End Region

#Region "Controlling Logic"

    Private Sub PopulateCompanyDropDowns()
        Me.UserCompanyMultipleDrop.SetControl(False, Me.UserCompanyMultipleDrop.MODES.NEW_MODE, True, Nothing, , True)
        If Not Me.State.MyBO Is Nothing Then
            Me.UserCompanyMultipleDrop.SelectedGuid = Me.State.MyBO.CompanyId
        End If
    End Sub

    Protected Sub EnableDisableFields()

        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)

        'New With Copy Button
        If Me.State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        End If
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Code", Me.lblCancCode)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.lblCancDesc)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RefundComputeMethodId", Me.labelRefundComputeMethod)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RefundDestinationId", Me.labelRefundDestination)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "InputAmtReqId", Me.labelInputAmtReq)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DisplayCodeId", Me.labelDisplayCode)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DefRefundPaymentMethodId", Me.labelDefRefPaymentMethod)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "IsLawful", Me.labelIsLawful)
        BindBOPropertyToLabel(State.MyBO, "BenefitCancelReasonCode", lblBenefitCancelCode)
        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub


    Protected Sub PopulateFormFromBOs()
        With Me.State.MyBO
            Me.PopulateControlFromBOProperty(Me.TextboxCode, .Code)
            Me.PopulateControlFromBOProperty(Me.TextboxDescription, .Description)

            Me.SetSelectedItem(Me.cboRefundComputeMethod, .RefundComputeMethodId)
            Me.SetSelectedItem(Me.cboRefundDestination, .RefundDestinationId)
            Me.SetSelectedItem(Me.cboInputAmtReq, .InputAmtReqId)
            Me.SetSelectedItem(Me.cboDisplayCode, .DisplayCodeId)
            Me.SetSelectedItem(Me.cboDefRefPaymentMethod, .DefRefundPaymentMethodId)
            If Not .IsLawful Is Nothing Then
                Me.SetSelectedItem(Me.cboIsLawful, .IsLawful)
            Else
                Me.SetSelectedItem(Me.cboIsLawful, "YESNO-N")
            End If
            Me.PopulateControlFromBOProperty(Me.txtBenefitCancelCode, .BenefitCancelReasonCode)
        End With

    End Sub

    Protected Sub PopulateBOsFormFrom()
        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO, "Code", Me.TextboxCode)
            Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.TextboxDescription)
            Me.PopulateBOProperty(Me.State.MyBO, "RefundComputeMethodId", Me.cboRefundComputeMethod)
            Me.PopulateBOProperty(Me.State.MyBO, "RefundDestinationId", Me.cboRefundDestination)
            Me.PopulateBOProperty(Me.State.MyBO, "InputAmtReqId", Me.cboInputAmtReq)
            Me.PopulateBOProperty(Me.State.MyBO, "DisplayCodeId", Me.cboDisplayCode)
            Me.PopulateBOProperty(Me.State.MyBO, "DefRefundPaymentMethodId", Me.cboDefRefPaymentMethod)
            Me.PopulateBOProperty(Me.State.MyBO, "IsLawful", Me.cboIsLawful, False, True)
            PopulateBOProperty(Me.State.MyBO, "BenefitCancelReasonCode", Me.txtBenefitCancelCode)

            If Me.State.MyBO.IsNew And ElitaPlusIdentity.Current.ActiveUser.Companies.Count > 1 Then
                Me.State.MyBO.CompanyId = Me.UserCompanyMultipleDrop.SelectedGuid
            End If
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub


    Protected Sub CreateNew()

        Me.State.ScreenSnapShotBO = Nothing
        Me.State.MyBO = New CancellationReason
        ClearAll()
        Me.State.cancellationReasonChanged = False
        Me.SetButtonsState(Me.State.MyBO.IsNew)
        Me.PopulateAll()
        'Me.PopulateFormFromBOs()
        'Me.EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        Me.PopulateBOsFormFrom()
        'Me.State.MyBO = New CancellationReason

        Dim newObj As New CancellationReason
        newObj.Copy(Me.State.MyBO)

        Me.State.MyBO = newObj

        'Me.State.MyBO.Id = Guid.Empty
        'Me.State.MyBO.IsNew = True

        With Me.State.MyBO '(newObj)
            .Code = Nothing
        End With

        ' Me.EnableDisableFields()
        Me.SetButtonsState(Me.State.MyBO.IsNew)
        'create the backup copy for undo
        Me.State.ScreenSnapShotBO = New CancellationReason
        Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

        'If Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_OK Then
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            Select Case Me.State.actionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.State.MyBO.Save()
                    Me.State.cancellationReasonChanged = True
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.cancellationReasonChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.State.MyBO.Save()
                    Me.State.cancellationReasonChanged = True
                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.State.MyBO.Save()
                    Me.State.cancellationReasonChanged = True
                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNewWithCopy()
            End Select
            'ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_CANCEL Then
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.actionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.cancellationReasonChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.CreateNewWithCopy()
            End Select
        End If

        'Clean after consuming the action
        Me.State.actionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub

#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.actionInProgress = ElitaPlusPage.DetailPageCommand.Back

                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM,
                                               Me.HiddenSaveChangesPromptResponse)
                Me.State.actionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.cancellationReasonChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                If Me.State.MyBO.IsNew Then
                    Me.State.MyBO.CompanyId = Me.UserCompanyMultipleDrop.SelectedGuid
                End If
                Me.State.MyBO.Save()
                Me.State.cancellationReasonChanged = False
                Me.PopulateAll()
                Me.EnableDisableFields()
                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New CancellationReason(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New CancellationReason
            End If
            PopulateAll()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            Me.State.MyBO.Delete()
            Me.State.MyBO.Save()
            Me.State.cancellationReasonChanged = True
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.cancellationReasonChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.State.MyBO.RejectChanges()
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            'Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.actionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.CreateNew()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


    Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.actionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                Me.CreateNewWithCopy()

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


#End Region

#Region "Gui-Validation"

    Private Sub SetButtonsState(ByVal bIsNew As Boolean)
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

    Private Sub UserControlAvailableExcludeRoles_Attach(ByVal aSrc As Generic.UserControlAvailableSelected_New, ByVal attachedList As System.Collections.ArrayList) Handles UserControlAvailableExcludeRoles.Attach
        Try
            If attachedList.Count > 0 Then
                Me.State.MyBO.AttachRoles(attachedList)
                'Me.PopulateDetailMfgGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UserControlAvailableExcludeRoles_Detach(ByVal aSrc As Generic.UserControlAvailableSelected_New, ByVal detachedList As System.Collections.ArrayList) Handles UserControlAvailableExcludeRoles.Detach
        Try
            If detachedList.Count > 0 Then
                Me.State.MyBO.DetachRoles(detachedList)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Populate"


    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("CancellationReason_List")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CancellationReason_List")
            End If
        End If
    End Sub
    Sub PopulateUserControlAvailableSelectedRoles()
        Me.UserControlAvailableExcludeRoles.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableExcludeRoles, False)
        Dim oDealer As Dealer
        Dim CountryId As Guid

        With Me.State.MyBO
            If Not .Id.Equals(Guid.Empty) Then

                Dim availableDv As DataView = .GetAvailableRoles
                Dim selectedDv As DataView = .GetSelectedRoles
                Me.UserControlAvailableExcludeRoles.SetAvailableData(availableDv, LookupListNew.COL_CODE_AND_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                Me.UserControlAvailableExcludeRoles.SetSelectedData(selectedDv, LookupListNew.COL_CODE_AND_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                ControlMgr.SetVisibleControl(Me, UserControlAvailableExcludeRoles, True)

            End If
        End With

    End Sub

    Protected Sub PopulateDropdowns()
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", langId, True)
        Dim yesNoLkL As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
        ' Me.BindListControlToDataView(Me.cboInputAmtReq, yesNoLkL)
        Me.cboInputAmtReq.Populate(yesNoLkL, New PopulateOptions() With
           {
              .AddBlankItem = True
           })
        'Me.BindListControlToDataView(Me.cboDisplayCode, yesNoLkL)
        Me.cboDisplayCode.Populate(yesNoLkL, New PopulateOptions() With
           {
              .AddBlankItem = True
           })
        ' Me.BindListControlToDataView(Me.cboRefundComputeMethod, LookupListNew.GetRefundComputeMethodLookupList(langId, True)) 'RMETH
        Me.cboRefundComputeMethod.Populate(CommonConfigManager.Current.ListManager.GetList("RMETH", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
           {
              .AddBlankItem = True
           })
        '  Me.BindListControlToDataView(Me.cboRefundDestination, LookupListNew.GetRefundDestinationLookupList(langId, True)) 'REFDS
        Me.cboRefundDestination.Populate(CommonConfigManager.Current.ListManager.GetList("REFDS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
           {
              .AddBlankItem = True
           })
        'Me.cboIsLawful.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
        Me.cboIsLawful.Populate(yesNoLkL, New PopulateOptions() With
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
        listcontext.CompanyId = Me.State.MyBO.CompanyId
        listcontext.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        listcontext.UserId = ElitaPlusIdentity.Current.ActiveUser.Id
        Dim paymentLKL As ListItem() = CommonConfigManager.Current.ListManager.GetList("PaymentMethodByRoleCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
        Dim filteredList As ListItem() = (From x In paymentLKL
                                          Where Not x.Code = "CTT" Or Not x.Code = "PYO"
                                          Select x).ToArray()
        Me.cboDefRefPaymentMethod.Populate(filteredList, New PopulateOptions() With
             {
            .AddBlankItem = True
             })
    End Sub

    Private Sub PopulateAll()
        If Me.State.cancellationReasonChanged = True Then
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

            If GetSelectedItem(Me.cboRefundDestination).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_REFUND_DESTINATION, Codes.REFUND_DESTINATION__CUSTOMER)) Then
                ControlMgr.SetEnableControl(Me, labelDefRefPaymentMethod, True)
                ControlMgr.SetEnableControl(Me, cboDefRefPaymentMethod, True)

                PopulatePaymentMethodDropDown()
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region


End Class
