Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Threading
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Partial Class CoverageLossForm
    Inherits ElitaPlusPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ErrorCtrl As ErrorController
    Protected WithEvents UserControlAvailableSelectedCauseOfLoss As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected
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
    Public Const URL As String = "CoverageLossForm.aspx"
    Private Const DEFAULT_FLAG As String = "Y"
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As CoverageType
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As CoverageType, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"

    Class MyState
        Public MyBO As CoverageType
        Public ScreenSnapShotBO As CoverageType
        Public SelectedSCId As Guid = Guid.Empty
        Public CoverageTypeId As Guid = Guid.Empty
        Public CoverageLoss As CoverageLoss
        Public SortExpressionDetailGrid As String = ServiceGroup.RiskTypeManufacturerDataView.RISK_TYPE_COL_NAME
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        Public IsNew As Boolean = False
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
                State.MyBO = New CoverageType(CType(CallingParameters, Guid))
            Else
                State.IsNew = True
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            ErrorCtrl.Clear_Hide()
            If Not IsPostBack Then
                MenuEnabled = False
                If State.MyBO Is Nothing Then
                    State.MyBO = New CoverageType
                    'Me.State.IsNew = True
                Else
                    PopulateFormFromBOs()
                End If
                PopulateDropdowns()
                ' Me.PopulateFormFromBOs()
                EnableDisableFields()
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub
#End Region

#Region "Controlling Logic"

    Protected Sub EnableDisableFields()
        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetVisibleControl(Me, cboCoverageType, False)
        ControlMgr.SetVisibleControl(Me, txtCoverageType, True)
        ControlMgr.SetEnableControl(Me, txtCoverageType, False)
        ControlMgr.SetEnableControl(Me, btnSave_WRITE, True)

        'Now disable depebding on the object state
        If State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetVisibleControl(Me, cboCoverageType, True)
            ControlMgr.SetVisibleControl(Me, txtCoverageType, False)
            'ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
        End If

    End Sub

    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, "CoverageTypeId", LabelCoverageType)
    End Sub

#Region "Detail Grid"

    Sub PopulateUserControlAvailableSelectedCauseOfLoss()
        UserControlAvailableSelectedCauseOfLoss.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedCauseOfLoss, False)

        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
        oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        oListContext.CoverageTypeId = State.MyBO.Id
        oListContext.LanguageId = Thread.CurrentPrincipal.GetLanguageId()

        Dim availableLst As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="GetAvailableCauseOfLossByCompanyGroup", context:=oListContext)
        Dim selectedLst As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="GetSelectedCauseOfLossByCompanyGroup", context:=oListContext)

        Dim availableDv As DataView = State.MyBO.GetAvailableCausesOfLoss(State.MyBO.Id)
        Dim selectedDv As DataView = State.MyBO.GetSelectedCausesOfLoss(State.MyBO.Id)
        UserControlAvailableSelectedCauseOfLoss.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
        UserControlAvailableSelectedCauseOfLoss.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)

        'Me.cboDefaultValue.Populate(selectedLst, New PopulateOptions() With
        '        {
        '            .AddBlankItem = True
        '        })

        BindListControlToDataView(cboDefaultValue, selectedDv, , , True)

        FindDefaultValue()
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedCauseOfLoss, True)
    End Sub

    Sub PopulateDetailControls()
        PopulateUserControlAvailableSelectedCauseOfLoss()
    End Sub
#End Region

    Protected Sub PopulateFormFromBOs()
        PopulateControlFromBOProperty(txtCoverageType, State.MyBO.GetCoverageTypeDescription(State.MyBO.Id))
        PopulateDetailControls()

    End Sub

    Protected Sub PopulateDropdowns()
        'Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        'Dim oCompanyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
        oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        oListContext.LanguageId = Thread.CurrentPrincipal.GetLanguageId()
        ' -- To-Do Code Changes
        'oListContext.WithoutLoss = "Y"

        Dim coverageLst As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CoverageTypeByCompanyGroup", context:=oListContext)
        cboCoverageType.Populate(coverageLst, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

        'Me.BindListControlToDataView(Me.cboCoverageType, LookupListNew.GetCoverageTypeByCompanyGroupLookupListNotInCovLoss(langId, oCompanyGroupId), , , True)

        SetSelectedItem(cboCoverageType, State.CoverageTypeId)
    End Sub

    Protected Sub PopulateBOsFromForm()
        If (cboDefaultValue.SelectedIndex >= 0) Then
            State.MyBO.AvailableDefaultValue = cboDefaultValue.SelectedValue
        Else
            State.MyBO.AvailableDefaultValue = Guid.Empty.ToString
        End If

    End Sub


    Protected Sub CreateNew()
        State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        State.MyBO = New CoverageType
        State.IsNew = True
        PopulateFormFromBOs()
        EnableDisableFields()
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                State.MyBO.Save()
            End If
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ErrorCtrl.AddErrorAndShow(State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Private Sub FindDefaultValue()

        State.CoverageLoss = State.MyBO.AssociatedCoveragesLoss.FindDefault
        If State.CoverageLoss IsNot Nothing Then
            SetSelectedItem(cboDefaultValue, State.CoverageLoss.CauseOfLossId)
        End If

    End Sub

#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = ErrorCtrl.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                State.MyBO.Save()
                State.IsNew = False
                State.HasDataChanged = True
                PopulateFormFromBOs()
                EnableDisableFields()
                DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                If UserControlAvailableSelectedCauseOfLoss.SelectedList.Count = 0 Then
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                End If
            Else
                DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New CoverageType(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                State.MyBO = New CoverageType
            End If
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


#End Region

#Region "Page Control Events"

    Private Sub cboCoverageType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboCoverageType.SelectedIndexChanged
        'Me.State.MyBO = Nothing
        State.MyBO = New CoverageType(GetSelectedItem(cboCoverageType))
        State.IsNew = False
        PopulateFormFromBOs()
    End Sub

    Private Sub cboDefaultValue_SelectedIndexChanged1(sender As Object, e As System.EventArgs) Handles cboDefaultValue.SelectedIndexChanged
        State.CoverageLoss = State.MyBO.AssociatedCoveragesLoss.FindDefault
        If State.CoverageLoss IsNot Nothing Then
            State.CoverageLoss.DefaultFlag = "N"
        End If
        'Dim causeOfLossId As Guid = Me.GetSelectedItem(Me.cboDefaultValue)
        State.CoverageLoss = State.MyBO.AssociatedCoveragesLoss.FindById(GetSelectedItem(cboDefaultValue))
        If State.CoverageLoss IsNot Nothing Then
            State.CoverageLoss.DefaultFlag = "Y"
        End If
    End Sub
#End Region

#Region "Handle-Drop"


#End Region

#Region "Attach - Detach Event Handlers"

    Private Sub UserControlAvailableSelectedCauseOfLoss_Attach(aSrc As Generic.UserControlAvailableSelected, attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedCauseOfLoss.Attach
        Try
            If attachedList.Count > 0 Then
                State.MyBO.AttachCoverageLoss(attachedList)
                Dim oItem As ListItem
                cboDefaultValue.Items.Clear()
                cboDefaultValue.Items.Add(New ListItem("", Guid.Empty.ToString))
                For Each oItem In UserControlAvailableSelectedCauseOfLoss.SelectedListListBox.Items
                    cboDefaultValue.Items.Add(New ListItem(oItem.Text, oItem.Value.ToString))
                Next
                FindDefaultValue()
                ControlMgr.SetEnableControl(Me, btnSave_WRITE, True)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedCauseOfLoss_Detach(aSrc As Generic.UserControlAvailableSelected, detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedCauseOfLoss.Detach

        Dim bo As New CoverageLoss

        Try
            If detachedList.Count > 0 Then
                State.CoverageLoss = State.MyBO.AssociatedCoveragesLoss.FindDefault
                Dim oItem As String
                For Each oItem In detachedList
                    If State.CoverageLoss IsNot Nothing AndAlso State.CoverageLoss.CauseOfLossId = New Guid(oItem) Then
                        SetSelectedItem(cboDefaultValue, Guid.Empty)
                        State.CoverageLoss.DefaultFlag = "N"
                    End If
                    cboDefaultValue.Items.Remove(oItem)
                Next
            End If
            State.MyBO.DetachCoverageLoss(detachedList)
            If State.IsNew AndAlso UserControlAvailableSelectedCauseOfLoss.SelectedList.Count = 0 Then
                ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region


End Class
