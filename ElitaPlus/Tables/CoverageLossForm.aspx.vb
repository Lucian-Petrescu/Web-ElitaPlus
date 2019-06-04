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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As CoverageType, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = New CoverageType(CType(Me.CallingParameters, Guid))
            Else
                Me.State.IsNew = True
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            Me.ErrorCtrl.Clear_Hide()
            If Not Me.IsPostBack Then
                Me.MenuEnabled = False
                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New CoverageType
                    'Me.State.IsNew = True
                Else
                    Me.PopulateFormFromBOs()
                End If
                Me.PopulateDropdowns()
                ' Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)
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
        If Me.State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetVisibleControl(Me, cboCoverageType, True)
            ControlMgr.SetVisibleControl(Me, txtCoverageType, False)
            'ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
        End If

    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CoverageTypeId", Me.LabelCoverageType)
    End Sub

#Region "Detail Grid"

    Sub PopulateUserControlAvailableSelectedCauseOfLoss()
        Me.UserControlAvailableSelectedCauseOfLoss.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedCauseOfLoss, False)

        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
        oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        oListContext.CoverageTypeId = Me.State.MyBO.Id
        oListContext.LanguageId = Thread.CurrentPrincipal.GetLanguageId()

        Dim availableLst As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="GetAvailableCauseOfLossByCompanyGroup", context:=oListContext)
        Dim selectedLst As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="GetSelectedCauseOfLossByCompanyGroup", context:=oListContext)

        Dim availableDv As DataView = Me.State.MyBO.GetAvailableCausesOfLoss(Me.State.MyBO.Id)
        Dim selectedDv As DataView = Me.State.MyBO.GetSelectedCausesOfLoss(Me.State.MyBO.Id)
        Me.UserControlAvailableSelectedCauseOfLoss.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
        Me.UserControlAvailableSelectedCauseOfLoss.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)

        Me.cboDefaultValue.Populate(selectedLst, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

        'Me.BindListControlToDataView(cboDefaultValue, selectedDv, , , True)

        FindDefaultValue()
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedCauseOfLoss, True)
    End Sub

    Sub PopulateDetailControls()
        PopulateUserControlAvailableSelectedCauseOfLoss()
    End Sub
#End Region

    Protected Sub PopulateFormFromBOs()
        Me.PopulateControlFromBOProperty(Me.txtCoverageType, Me.State.MyBO.GetCoverageTypeDescription(Me.State.MyBO.Id))
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
        Me.cboCoverageType.Populate(coverageLst, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

        'Me.BindListControlToDataView(Me.cboCoverageType, LookupListNew.GetCoverageTypeByCompanyGroupLookupListNotInCovLoss(langId, oCompanyGroupId), , , True)

        Me.SetSelectedItem(Me.cboCoverageType, Me.State.CoverageTypeId)
    End Sub

    Protected Sub PopulateBOsFromForm()
        If (Me.cboDefaultValue.SelectedIndex >= 0) Then
            Me.State.MyBO.AvailableDefaultValue = Me.cboDefaultValue.SelectedValue
        Else
            Me.State.MyBO.AvailableDefaultValue = Guid.Empty.ToString
        End If

    End Sub


    Protected Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        Me.State.MyBO = New CoverageType
        Me.State.IsNew = True
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                Me.State.MyBO.Save()
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ErrorCtrl.AddErrorAndShow(Me.State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Private Sub FindDefaultValue()

        Me.State.CoverageLoss = Me.State.MyBO.AssociatedCoveragesLoss.FindDefault
        If Not Me.State.CoverageLoss Is Nothing Then
            Me.SetSelectedItem(Me.cboDefaultValue, Me.State.CoverageLoss.CauseOfLossId)
        End If

    End Sub

#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.ErrorCtrl.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                Me.State.MyBO.Save()
                Me.State.IsNew = False
                Me.State.HasDataChanged = True
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                If Me.UserControlAvailableSelectedCauseOfLoss.SelectedList.Count = 0 Then
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                End If
            Else
                Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New CoverageType(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New CoverageType
            End If
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


    Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.CreateNew()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


#End Region

#Region "Page Control Events"

    Private Sub cboCoverageType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCoverageType.SelectedIndexChanged
        'Me.State.MyBO = Nothing
        Me.State.MyBO = New CoverageType(Me.GetSelectedItem(Me.cboCoverageType))
        Me.State.IsNew = False
        Me.PopulateFormFromBOs()
    End Sub

    Private Sub cboDefaultValue_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDefaultValue.SelectedIndexChanged
        Me.State.CoverageLoss = Me.State.MyBO.AssociatedCoveragesLoss.FindDefault
        If Not Me.State.CoverageLoss Is Nothing Then
            Me.State.CoverageLoss.DefaultFlag = "N"
        End If
        'Dim causeOfLossId As Guid = Me.GetSelectedItem(Me.cboDefaultValue)
        Me.State.CoverageLoss = Me.State.MyBO.AssociatedCoveragesLoss.FindById(Me.GetSelectedItem(Me.cboDefaultValue))
        If Not Me.State.CoverageLoss Is Nothing Then
            Me.State.CoverageLoss.DefaultFlag = "Y"
        End If
    End Sub
#End Region

#Region "Handle-Drop"


#End Region

#Region "Attach - Detach Event Handlers"

    Private Sub UserControlAvailableSelectedCauseOfLoss_Attach(ByVal aSrc As Generic.UserControlAvailableSelected, ByVal attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedCauseOfLoss.Attach
        Try
            If attachedList.Count > 0 Then
                Me.State.MyBO.AttachCoverageLoss(attachedList)
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
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedCauseOfLoss_Detach(ByVal aSrc As Generic.UserControlAvailableSelected, ByVal detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedCauseOfLoss.Detach

        Dim bo As New CoverageLoss

        Try
            If detachedList.Count > 0 Then
                Me.State.CoverageLoss = Me.State.MyBO.AssociatedCoveragesLoss.FindDefault
                Dim oItem As String
                For Each oItem In detachedList
                    If Not Me.State.CoverageLoss Is Nothing AndAlso Me.State.CoverageLoss.CauseOfLossId = New Guid(oItem) Then
                        Me.SetSelectedItem(Me.cboDefaultValue, Guid.Empty)
                        Me.State.CoverageLoss.DefaultFlag = "N"
                    End If
                    Me.cboDefaultValue.Items.Remove(oItem)
                Next
            End If
            Me.State.MyBO.DetachCoverageLoss(detachedList)
            If Me.State.IsNew AndAlso Me.UserControlAvailableSelectedCauseOfLoss.SelectedList.Count = 0 Then
                ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region


End Class
