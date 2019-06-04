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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As CompanyGroup, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = New CompanyGroup(CType(Me.CallingParameters, Guid))
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
                    Me.State.MyBO = New CompanyGroup
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
        ControlMgr.SetVisibleControl(Me, cboCompanyGroupId, False)
        ControlMgr.SetVisibleControl(Me, txtCompanyGroup, True)
        ControlMgr.SetEnableControl(Me, txtCompanyGroup, False)
        ControlMgr.SetEnableControl(Me, btnSave_WRITE, True)

        'Now disable depebding on the object state
        If Me.State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetVisibleControl(Me, cboCompanyGroupId, True)
            ControlMgr.SetVisibleControl(Me, txtCompanyGroup, False)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
        End If

    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CoverageTypeId", Me.LabelCompanyGroup)
    End Sub

#Region "Detail Control"

    Sub PopulateDetailControls()

        Me.UserControlAvailableSelectedCoverageType.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedCoverageType, False)
        Dim availableDv As DataView = Me.State.MyBO.GetAvailableCoverageType(Me.State.MyBO.Id)
        Dim selectedDv As DataView = Me.State.MyBO.GetSelectedCoverageType(Me.State.MyBO.Id)
        If Not Me.State.IsNew Then
            Me.UserControlAvailableSelectedCoverageType.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            Me.UserControlAvailableSelectedCoverageType.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
        Else
            availableDv = Nothing
            selectedDv = Nothing
            Me.UserControlAvailableSelectedCoverageType.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            Me.UserControlAvailableSelectedCoverageType.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
        End If
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedCoverageType, True)

    End Sub
#End Region

    Protected Sub PopulateFormFromBOs()
        Me.PopulateControlFromBOProperty(Me.txtCompanyGroup, Me.State.MyBO.GetCompanyGroupDescription(Me.State.MyBO.Id))
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
        Me.cboCompanyGroupId.Populate(FilteredRecord, New PopulateOptions() With
                                  {
                                  .AddBlankItem = True
                                  })
        'Me.SetSelectedItem(Me.cboCompanyGroupId, Me.State.MyBO.Id)
    End Sub

    Protected Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        Me.State.MyBO = New CompanyGroup
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

#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
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
            If Me.State.MyBO.IsDirty Then
                Me.State.MyBO.Save()
                Me.State.IsNew = False
                Me.State.HasDataChanged = True
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                If Me.UserControlAvailableSelectedCoverageType.SelectedList.Count = 0 Then
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
                Me.State.MyBO = New CompanyGroup(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New CompanyGroup
            End If
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


    Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
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

    Private Sub cboCompanyGroupId_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCompanyGroupId.SelectedIndexChanged
        Me.State.MyBO = New CompanyGroup(Me.GetSelectedItem(Me.cboCompanyGroupId))
        Me.State.IsNew = False
        Me.PopulateFormFromBOs()
    End Sub

#End Region

#Region "Handle-Drop"


#End Region

#Region "Attach - Detach Event Handlers"

    Private Sub UserControlAvailableSelectedCoverageType_Attach(ByVal aSrc As Generic.UserControlAvailableSelected, ByVal attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedCoverageType.Attach
        Try
            If attachedList.Count > 0 Then
                Me.State.MyBO.AttachCoverageType(attachedList)
                ControlMgr.SetEnableControl(Me, btnSave_WRITE, True)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedCoverageType_Detach(ByVal aSrc As Generic.UserControlAvailableSelected, ByVal detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedCoverageType.Detach

        Dim bo As New CoverageLoss
        Try
            If detachedList.Count > 0 Then
                Me.State.MyBO.DetachCoverageType(detachedList)
            End If
            If Me.State.IsNew AndAlso Me.UserControlAvailableSelectedCoverageType.SelectedList.Count = 0 Then
                ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region



End Class

