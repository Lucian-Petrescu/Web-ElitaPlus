Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Microsoft.VisualBasic
Partial Class CoverageByCompanyGroupForm
    Inherits ElitaPlusPage


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ErrorCtrl As ErrorController
    Protected WithEvents UserControlAvailableSelectedCoverageType As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected

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
    Public Const URL As String = "CoverageByCompanyGroupForm.aspx"
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As CompanyGroup
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As CompanyGroup, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"

    Class MyState
        Public MyBO As CompanyGroup
        Public ScreenSnapShotBO As CompanyGroup
        Public SelectedSCId As Guid = Guid.Empty
        'Public CompanyGrpId As Guid = Guid.Empty
        'Public CoverageLoss As CoverageLoss
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
                State.MyBO = New CompanyGroup(CType(CallingParameters, Guid))
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
                    State.MyBO = New CompanyGroup
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
        ControlMgr.SetVisibleControl(Me, cboCompanyGroupId, False)
        ControlMgr.SetVisibleControl(Me, txtCompanyGroup, True)
        ControlMgr.SetEnableControl(Me, txtCompanyGroup, False)
        ControlMgr.SetEnableControl(Me, btnSave_WRITE, True)

        'Now disable depebding on the object state
        If State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetVisibleControl(Me, cboCompanyGroupId, True)
            ControlMgr.SetVisibleControl(Me, txtCompanyGroup, False)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
        End If

    End Sub

    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, "CoverageTypeId", LabelCompanyGroup)
    End Sub

#Region "Detail Control"

    Sub PopulateDetailControls()

        UserControlAvailableSelectedCoverageType.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedCoverageType, False)
        Dim availableDv As DataView = State.MyBO.GetAvailableCoverageType(State.MyBO.Id)
        Dim selectedDv As DataView = State.MyBO.GetSelectedCoverageType(State.MyBO.Id)
        If Not State.IsNew Then
            UserControlAvailableSelectedCoverageType.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            UserControlAvailableSelectedCoverageType.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
        Else
            availableDv = Nothing
            selectedDv = Nothing
            UserControlAvailableSelectedCoverageType.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            UserControlAvailableSelectedCoverageType.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
        End If
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedCoverageType, True)

    End Sub
#End Region

    Protected Sub PopulateFormFromBOs()
        PopulateControlFromBOProperty(txtCompanyGroup, State.MyBO.GetCompanyGroupDescription(State.MyBO.Id))
        'Me.State.MyBO.GetCoverageTypeDescription(Me.State.MyBO.Id)
        PopulateDetailControls()

    End Sub


    Protected Sub PopulateDropdowns()
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        'Me.BindListControlToDataView(Me.cboCompanyGroupId, LookupListNew.DataView(LookupListNew.LK_COMPANY_GROUP))
        ' Me.BindListControlToDataView(Me.cboCompanyGroupId, LookupListNew.GetCompanyGroupNoptInUseLookupList(CoverageByCompanyGroup.GetUsedCompanyGroup), , , True) 'CompanyGroup
        Dim recordTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CompanyGroup", Thread.CurrentPrincipal.GetLanguageCode())
        Dim FilteredRecord As ListItem() = (From x In recordTypeLkl
                                            Where CoverageByCompanyGroup.GetUsedCompanyGroup.Contains(x.ListItemId)
                                            Select x).ToArray()
        cboCompanyGroupId.Populate(FilteredRecord, New PopulateOptions() With
                                  {
                                  .AddBlankItem = True
                                  })
        'Me.SetSelectedItem(Me.cboCompanyGroupId, Me.State.MyBO.Id)
    End Sub

    Protected Sub CreateNew()
        State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        State.MyBO = New CompanyGroup
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

#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
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
            If State.MyBO.IsDirty Then
                State.MyBO.Save()
                State.IsNew = False
                State.HasDataChanged = True
                PopulateFormFromBOs()
                EnableDisableFields()
                DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                If UserControlAvailableSelectedCoverageType.SelectedList.Count = 0 Then
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
                State.MyBO = New CompanyGroup(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                State.MyBO = New CompanyGroup
            End If
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
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

    Private Sub cboCompanyGroupId_SelectedIndexChanged1(sender As Object, e As System.EventArgs) Handles cboCompanyGroupId.SelectedIndexChanged
        State.MyBO = New CompanyGroup(GetSelectedItem(cboCompanyGroupId))
        State.IsNew = False
        PopulateFormFromBOs()
    End Sub

#End Region

#Region "Handle-Drop"


#End Region

#Region "Attach - Detach Event Handlers"

    Private Sub UserControlAvailableSelectedCoverageType_Attach(aSrc As Generic.UserControlAvailableSelected, attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedCoverageType.Attach
        Try
            If attachedList.Count > 0 Then
                State.MyBO.AttachCoverageType(attachedList)
                ControlMgr.SetEnableControl(Me, btnSave_WRITE, True)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedCoverageType_Detach(aSrc As Generic.UserControlAvailableSelected, detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedCoverageType.Detach

        Dim bo As New CoverageLoss
        Try
            If detachedList.Count > 0 Then
                State.MyBO.DetachCoverageType(detachedList)
            End If
            If State.IsNew AndAlso UserControlAvailableSelectedCoverageType.SelectedList.Count = 0 Then
                ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region



End Class

