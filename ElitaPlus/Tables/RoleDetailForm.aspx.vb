Imports System.Text.RegularExpressions
Imports System.Security.Cryptography.X509Certificates
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Public Class RoleDetailForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "


    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Const URL As String = "~/Tables/RoleDetailForm.aspx"

    Public Const PAGETAB As String = "ADMIN"
    Public Const PAGETITLE As String = "ROLES"
#End Region

#Region "Page State"

    Class MyState
        Public MyBO As Role

        Public RoleReturnObject As Tables.RoleListForm.RoleReturnType
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

#End Region

#Region "Page Events"

    Private Sub Page_PageCall(CallFromUrl As String, CallingParameters As Object) Handles MyBase.PageCall
        Try
            If Me.CallingParameters IsNot Nothing Then
                State.RoleReturnObject = CType(CallingParameters, Tables.RoleListForm.RoleReturnType)
                If (Not State.RoleReturnObject.RoleId.Equals(Guid.Empty)) Then
                    State.MyBO = New Role(State.RoleReturnObject.RoleId)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            MasterPage.MessageController.Clear()
            If (Not IsPostBack) Then
                'TODO Popup Configuration for Delete Message Box
                'lblCancelMessage.Text = TranslationBase.TranslateLabelOrMessage("MSG_CONFIRM_PROMPT")
                'btnModalCancelYes.Attributes.Add("onclick", "YesButtonClick();")

                If (State.MyBO Is Nothing) Then
                    State.MyBO = New Role()
                End If

                ' Populate Bread Crum
                UpdateBreadCrum()

                ' Populate Drop Downs
                PopulateDropdowns()
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
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub
#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFormFrom()
            State.RoleReturnObject.RoleId = State.MyBO.Id
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New PageReturnType(Of Tables.RoleListForm.RoleReturnType)(ElitaPlusPage.DetailPageCommand.Back, State.RoleReturnObject, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsFamilyDirty Then
                State.MyBO.Save()
                State.MyBO = New Role(State.MyBO.Id)
                State.HasDataChanged = True
                PopulateFormFromBOs()
                EnableDisableFields()
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New Role(State.MyBO.Id)
            Else
                State.MyBO = New Role()
            End If
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub DoDelete()
        State.MyBO.RolePermission.RevokeAll()
        State.MyBO.Delete()
        State.MyBO.Save()
        State.HasDataChanged = True
        ReturnToCallingPage(New PageReturnType(Of Tables.RoleListForm.RoleReturnType)(ElitaPlusPage.DetailPageCommand.Delete, State.RoleReturnObject, State.HasDataChanged))
    End Sub

    Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnRemovePermission_Click(sender As System.Object, e As System.EventArgs) Handles btnRemovePermission.Click
        Try
            If lstSelectedPermission.SelectedItem Is Nothing Then Exit Sub
            State.MyBO.RolePermission.Revoke(New Guid(lstSelectedPermission.SelectedItem.Value))
            RefreshPermissions()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnAddPermissionToSelected_Click(sender As System.Object, e As System.EventArgs) Handles btnAddPermissionToSelected.Click
        Try
            If lstAvailablePermission.SelectedItem Is Nothing Then Exit Sub
            State.MyBO.RolePermission.Grant(New Guid(lstAvailablePermission.SelectedItem.Value))
            RefreshPermissions()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Controlling Logic"

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Delete _
                AndAlso State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Expire Then
                State.MyBO.Save()
            End If
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New PageReturnType(Of Tables.RoleListForm.RoleReturnType)(ElitaPlusPage.DetailPageCommand.Back, State.RoleReturnObject, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ReturnToCallingPage(New PageReturnType(Of Tables.RoleListForm.RoleReturnType)(ElitaPlusPage.DetailPageCommand.Back, State.RoleReturnObject, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Delete
                    DoDelete()
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New PageReturnType(Of Tables.RoleListForm.RoleReturnType)(ElitaPlusPage.DetailPageCommand.Back, State.RoleReturnObject, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)
            End Select
        End If

        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Private Sub UpdateBreadCrum()
        MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) & ElitaBase.Sperator
        MasterPage.BreadCrum = MasterPage.BreadCrum & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        MasterPage.UsePageTabTitleInBreadCrum = False
    End Sub

    Private Sub PopulateDropdowns()
        Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim roleProvidersDv As DataView = LookupListNew.GetRoleProviderList(languageId) 'ROLE_PROVIDER
        'Dim yesNoDv As DataView = LookupListNew.GetYesNoLookupList(languageId)
        'Me.BindListControlToDataView(moIhqOnly, yesNoDv)
        moIhqOnly.Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With {
        .AddBlankItem = True
        })
        'Me.BindListControlToDataView(moRoleProvider, roleProvidersDv)
        moRoleProvider.Populate(CommonConfigManager.Current.ListManager.GetList("ROLE_PROVIDER", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With {
        .AddBlankItem = True
        })
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, "Code", moRoleCodeLabel)
        BindBOPropertyToLabel(State.MyBO, "Description", moRoleDescriptionLabel)
        BindBOPropertyToLabel(State.MyBO, "IhqOnly", moIhqOnlyLabel)
        BindBOPropertyToLabel(State.MyBO, "RoleProviderId", moRoleProviderLabel)
    End Sub

    Protected Sub PopulateFormFromBOs()
        With State.MyBO
            PopulateControlFromBOProperty(moRoleCode, .Code)
            PopulateControlFromBOProperty(moRoleDescription, .Description)
            PopulateControlFromBOProperty(moRoleProvider, .RoleProviderId)
            PopulateControlFromBOProperty(moIhqOnly, LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, .IhqOnly))

            RefreshPermissions()
        End With
    End Sub

    Private Sub RefreshPermissions()
        With State.MyBO
            ElitaPlusPage.BindListControlToDataView(lstSelectedPermission, .RolePermission.GetSelectedPermissions(), DALObjects.DALBase.COL_NAME_DESCRIPTION, DALObjects.DALBase.COL_NAME_ID, False)
            ElitaPlusPage.BindListControlToDataView(lstAvailablePermission, .RolePermission.GetAvailablePermissions(), DALObjects.DALBase.COL_NAME_DESCRIPTION, DALObjects.DALBase.COL_NAME_ID, False)
        End With
    End Sub

    Sub EnableDisableFields()
        ControlMgr.SetEnableControl(Me, btnBack, True)
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnSave_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)

        'Now disable depebding on the object state
        If State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
        End If

    End Sub

    Protected Sub CreateNew()
        State.MyBO = New Role()
        PopulateFormFromBOs()
        EnableDisableFields()
    End Sub

    Protected Sub PopulateBOsFormFrom()
        With State.MyBO
            PopulateBOProperty(State.MyBO, "Code", moRoleCode)
            PopulateBOProperty(State.MyBO, "Description", moRoleDescription)
            PopulateBOProperty(State.MyBO, "RoleProviderId", moRoleProvider)
            PopulateBOProperty(State.MyBO, "IhqOnly", LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, New Guid(GetSelectedValue(moIhqOnly))))
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

#End Region

End Class