Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Class VSCPlanForm
    Inherits ElitaPlusPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ErrorCtrl As ErrorController
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
    Public Shared URL As String = "VSCPlanForm.aspx"
    Public Const CODE_PROPERTY As String = "Code"
    Public Const DESCRIPTION_PROPERTY As String = "Description"
    Public Const RISKTYPEID_PROPERTY As String = "RiskTypeId"
    Public Const RISKGROUPID_PROPERTY As String = "RiskGroupId"
    Public Const ISWRAPPLAN_PROPERTY As String = "IsWrapPlan"
    Public Const VSCPlan_DIRTY_COLUMNS_COUNT As Integer = 3
    Private Const VSCPlan_LIST_FORM001 As String = "VSCPlan_LIST_FORM001" ' Maintain VSCPlan List Exception
    Public Const RISK_GRP_DESCRIPTION As String = "description"
    Public Const RISK_GRP_ID As String = "id"
    Public Const YES As String = "Y"
    Public Const NO As String = "N"
#End Region

#Region "Page State"

    Class MyState
        Public MyBO As VSCPlan
        Public ScreenSnapShotBO As VSCPlan
        Public IsNew As Boolean
        Public IsACopy As Boolean
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
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
                State.MyBO = New VSCPlan(CType(CallingParameters, Guid))
            Else
                State.IsNew = True
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As VSCPlan
        Public HasDataChanged As Boolean

        Public Sub New(LastOp As DetailPageCommand, curEditingBo As VSCPlan, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ErrorCtrl.Clear_Hide()
        Try
            If Not IsPostBack Then
                PopulateAll()
                MenuEnabled = False
                AddConfirmation(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                If State.MyBO Is Nothing Then
                    State.MyBO = New VSCPlan
                End If

                If State.IsNew = True Then
                    CreateNew()
                End If
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
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub
#End Region

#Region "Populate"

    Private Sub PopulateRiskGroups()
        'Dim dvRiskGrps As DataView
        'dvRiskGrps = LookupListNew.GetRiskGroupsList()
        'Me.BindListControlToDataView(cboRiskGroup, dvRiskGrps, RISK_GRP_DESCRIPTION, RISK_GRP_ID)

        Dim RiskGroups As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="RGRP",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

        cboRiskGroup.Populate(RiskGroups.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })

    End Sub

    Private Sub PopulateRiskTypes()
        'Dim dvRiskTypes As DataView
        'Dim oCompanyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        'dvRiskTypes = LookupListNew.GetRiskTypesList(oCompanyGroupId, New Guid(cboRiskGroup.SelectedItem.Value))
        'Me.BindListControlToDataView(cboRiskType, dvRiskTypes, RISK_GRP_DESCRIPTION, RISK_GRP_ID)

        Dim RiskTypes As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.RiskTypeByCompanyGroup,
                                                                context:=New ListContext() With
                                                                {
                                                                  .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                                                                })

        cboRiskType.Populate(RiskTypes.ToArray(),
                                 New PopulateOptions() With
                                 {
                                   .AddBlankItem = True
                                 })

    End Sub

    Private Sub PopulateAll()
        PopulateRiskGroups()
        PopulateRiskTypes()
    End Sub
#End Region

#Region "Controlling Logic"

    Private Sub cboRiskGroup_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboRiskGroup.SelectedIndexChanged
        PopulateRiskTypes()
    End Sub

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

    End Sub

    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, CODE_PROPERTY, lblCode)
        BindBOPropertyToLabel(State.MyBO, DESCRIPTION_PROPERTY, lblDesc)
        BindBOPropertyToLabel(State.MyBO, RISKTYPEID_PROPERTY, lblRiskType)
        BindBOPropertyToLabel(State.MyBO, RISKGROUPID_PROPERTY, lblRiskGroup)
        BindBOPropertyToLabel(State.MyBO, ISWRAPPLAN_PROPERTY, lblWrapPlan)
        ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateFormFromBOs()
        With State.MyBO
            PopulateControlFromBOProperty(txtCode, .Code)
            PopulateControlFromBOProperty(txtDesc, .Description)
            If .IsWrapPlan = YES Then
                PopulateControlFromBOProperty(chkWrapPlan, True)
            Else
                PopulateControlFromBOProperty(chkWrapPlan, False)
            End If
            PopulateControlFromBOProperty(cboRiskGroup, .RiskGroupId)
            'If cboRiskType.Items.Count < 0 Then
            PopulateRiskTypes()
            'End If
            PopulateControlFromBOProperty(cboRiskType, .RiskTypeId)
        End With

    End Sub

    Protected Sub PopulateBOsFromForm()
        Dim oCompanyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

        With State.MyBO
            PopulateBOProperty(State.MyBO, "Code", txtCode)
            PopulateBOProperty(State.MyBO, "Description", txtDesc)
            PopulateBOProperty(State.MyBO, "CompanyGroupId", oCompanyGroupId)
            If chkWrapPlan.Checked Then
                PopulateBOProperty(State.MyBO, "IsWrapPlan", "Y")
            Else
                PopulateBOProperty(State.MyBO, "IsWrapPlan", "N")
            End If
            PopulateBOProperty(State.MyBO, "RiskGroupId", cboRiskGroup)
            PopulateBOProperty(State.MyBO, "RiskTypeId", cboRiskType)
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub


    Protected Sub CreateNew()
        State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        State.MyBO = New VSCPlan
        PopulateAll()
        PopulateFormFromBOs()
        EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()

        PopulateBOsFromForm()

        Dim newObj As New VSCPlan
        newObj.Copy(State.MyBO)

        State.MyBO = newObj
        State.MyBO.Code = Nothing
        State.MyBO.Description = Nothing

        PopulateFormFromBOs()
        EnableDisableFields()

        'create the backup copy
        State.ScreenSnapShotBO = New VSCPlan
        State.ScreenSnapShotBO.Clone(State.MyBO)
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
                    ErrorCtrl.AddErrorAndShow(State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub

#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                Dim iDirtyCols As Integer
                iDirtyCols = State.MyBO.DirtyColumns.Count
                If iDirtyCols > VSCPlan_DIRTY_COLUMNS_COUNT Then
                    AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                End If
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
            AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = ErrorCtrl.Text
        End Try
    End Sub
    Private Sub btnCopy_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewWithCopy()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
    Private Sub btnDelete_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            State.MyBO.DeleteAndSave()
            State.HasDataChanged = True
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
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
            HandleErrors(ex, ErrorCtrl)
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
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnUndo_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New VSCPlan(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                CreateNew()
            End If
            PopulateAll()
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region


End Class
