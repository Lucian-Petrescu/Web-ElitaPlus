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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = New VSCPlan(CType(Me.CallingParameters, Guid))
            Else
                Me.State.IsNew = True
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As VSCPlan
        Public HasDataChanged As Boolean

        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As VSCPlan, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.ErrorCtrl.Clear_Hide()
        Try
            If Not Me.IsPostBack Then
                PopulateAll()
                Me.MenuEnabled = False
                Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New VSCPlan
                End If

                If Me.State.IsNew = True Then
                    CreateNew()
                End If
                Me.PopulateFormFromBOs()
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

#Region "Populate"

    Private Sub PopulateRiskGroups()
        'Dim dvRiskGrps As DataView
        'dvRiskGrps = LookupListNew.GetRiskGroupsList()
        'Me.BindListControlToDataView(cboRiskGroup, dvRiskGrps, RISK_GRP_DESCRIPTION, RISK_GRP_ID)

        Dim RiskGroups As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="RGRP",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

        Me.cboRiskGroup.Populate(RiskGroups.ToArray(),
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

        Me.cboRiskType.Populate(RiskTypes.ToArray(),
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

    Private Sub cboRiskGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRiskGroup.SelectedIndexChanged
        PopulateRiskTypes()
    End Sub

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

    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, CODE_PROPERTY, Me.lblCode)
        Me.BindBOPropertyToLabel(Me.State.MyBO, DESCRIPTION_PROPERTY, Me.lblDesc)
        Me.BindBOPropertyToLabel(Me.State.MyBO, RISKTYPEID_PROPERTY, Me.lblRiskType)
        Me.BindBOPropertyToLabel(Me.State.MyBO, RISKGROUPID_PROPERTY, Me.lblRiskGroup)
        Me.BindBOPropertyToLabel(Me.State.MyBO, ISWRAPPLAN_PROPERTY, Me.lblWrapPlan)
        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateFormFromBOs()
        With Me.State.MyBO
            Me.PopulateControlFromBOProperty(Me.txtCode, .Code)
            Me.PopulateControlFromBOProperty(Me.txtDesc, .Description)
            If .IsWrapPlan = YES Then
                Me.PopulateControlFromBOProperty(Me.chkWrapPlan, True)
            Else
                Me.PopulateControlFromBOProperty(Me.chkWrapPlan, False)
            End If
            Me.PopulateControlFromBOProperty(Me.cboRiskGroup, .RiskGroupId)
            'If cboRiskType.Items.Count < 0 Then
            PopulateRiskTypes()
            'End If
            Me.PopulateControlFromBOProperty(Me.cboRiskType, .RiskTypeId)
        End With

    End Sub

    Protected Sub PopulateBOsFromForm()
        Dim oCompanyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO, "Code", Me.txtCode)
            Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.txtDesc)
            Me.PopulateBOProperty(Me.State.MyBO, "CompanyGroupId", oCompanyGroupId)
            If chkWrapPlan.Checked Then
                Me.PopulateBOProperty(Me.State.MyBO, "IsWrapPlan", "Y")
            Else
                Me.PopulateBOProperty(Me.State.MyBO, "IsWrapPlan", "N")
            End If
            Me.PopulateBOProperty(Me.State.MyBO, "RiskGroupId", Me.cboRiskGroup)
            Me.PopulateBOProperty(Me.State.MyBO, "RiskTypeId", Me.cboRiskType)
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub


    Protected Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        Me.State.MyBO = New VSCPlan
        Me.PopulateAll()
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()

        Me.PopulateBOsFromForm()

        Dim newObj As New VSCPlan
        newObj.Copy(Me.State.MyBO)

        Me.State.MyBO = newObj
        Me.State.MyBO.Code = Nothing
        Me.State.MyBO.Description = Nothing

        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()

        'create the backup copy
        Me.State.ScreenSnapShotBO = New VSCPlan
        Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
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
                    Me.ErrorCtrl.AddErrorAndShow(Me.State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub

#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                Dim iDirtyCols As Integer
                iDirtyCols = Me.State.MyBO.DirtyColumns.Count
                If iDirtyCols > VSCPlan_DIRTY_COLUMNS_COUNT Then
                    Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                End If
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
            Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.ErrorCtrl.Text
        End Try
    End Sub
    Private Sub btnCopy_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                Me.CreateNewWithCopy()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
    Private Sub btnDelete_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            Me.State.MyBO.DeleteAndSave()
            Me.State.HasDataChanged = True
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
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
            Me.HandleErrors(ex, Me.ErrorCtrl)
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
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnUndo_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New VSCPlan(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                CreateNew()
            End If
            PopulateAll()
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region


End Class
