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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingParameters As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                Me.State.RoleReturnObject = CType(CallingParameters, Tables.RoleListForm.RoleReturnType)
                If (Not Me.State.RoleReturnObject.RoleId.Equals(Guid.Empty)) Then
                    Me.State.MyBO = New Role(Me.State.RoleReturnObject.RoleId)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            Me.MasterPage.MessageController.Clear()
            If (Not Me.IsPostBack) Then
                'TODO Popup Configuration for Delete Message Box
                'lblCancelMessage.Text = TranslationBase.TranslateLabelOrMessage("MSG_CONFIRM_PROMPT")
                'btnModalCancelYes.Attributes.Add("onclick", "YesButtonClick();")

                If (Me.State.MyBO Is Nothing) Then
                    Me.State.MyBO = New Role()
                End If

                ' Populate Bread Crum
                UpdateBreadCrum()

                ' Populate Drop Downs
                PopulateDropdowns()
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub
#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFormFrom()
            Me.State.RoleReturnObject.RoleId = Me.State.MyBO.Id
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New PageReturnType(Of Tables.RoleListForm.RoleReturnType)(ElitaPlusPage.DetailPageCommand.Back, Me.State.RoleReturnObject, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsFamilyDirty Then
                Me.State.MyBO.Save()
                Me.State.MyBO = New Role(Me.State.MyBO.Id)
                Me.State.HasDataChanged = True
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New Role(Me.State.MyBO.Id)
            Else
                Me.State.MyBO = New Role()
            End If
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub DoDelete()
        Me.State.MyBO.RolePermission.RevokeAll()
        Me.State.MyBO.Delete()
        Me.State.MyBO.Save()
        Me.State.HasDataChanged = True
        Me.ReturnToCallingPage(New PageReturnType(Of Tables.RoleListForm.RoleReturnType)(ElitaPlusPage.DetailPageCommand.Delete, Me.State.RoleReturnObject, Me.State.HasDataChanged))
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.CreateNew()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnRemovePermission_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemovePermission.Click
        Try
            If lstSelectedPermission.SelectedItem Is Nothing Then Exit Sub
            Me.State.MyBO.RolePermission.Revoke(New Guid(lstSelectedPermission.SelectedItem.Value))
            RefreshPermissions()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnAddPermissionToSelected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPermissionToSelected.Click
        Try
            If lstAvailablePermission.SelectedItem Is Nothing Then Exit Sub
            Me.State.MyBO.RolePermission.Grant(New Guid(lstAvailablePermission.SelectedItem.Value))
            RefreshPermissions()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Controlling Logic"

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Delete _
                AndAlso Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Expire Then
                Me.State.MyBO.Save()
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New PageReturnType(Of Tables.RoleListForm.RoleReturnType)(ElitaPlusPage.DetailPageCommand.Back, Me.State.RoleReturnObject, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New PageReturnType(Of Tables.RoleListForm.RoleReturnType)(ElitaPlusPage.DetailPageCommand.Back, Me.State.RoleReturnObject, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Delete
                    DoDelete()
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New PageReturnType(Of Tables.RoleListForm.RoleReturnType)(ElitaPlusPage.DetailPageCommand.Back, Me.State.RoleReturnObject, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)
            End Select
        End If

        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Private Sub UpdateBreadCrum()
        Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) & ElitaBase.Sperator
        Me.MasterPage.BreadCrum = Me.MasterPage.BreadCrum & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
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
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Code", Me.moRoleCodeLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.moRoleDescriptionLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "IhqOnly", Me.moIhqOnlyLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RoleProviderId", Me.moRoleProviderLabel)
    End Sub

    Protected Sub PopulateFormFromBOs()
        With Me.State.MyBO
            Me.PopulateControlFromBOProperty(Me.moRoleCode, .Code)
            Me.PopulateControlFromBOProperty(Me.moRoleDescription, .Description)
            Me.PopulateControlFromBOProperty(Me.moRoleProvider, .RoleProviderId)
            Me.PopulateControlFromBOProperty(Me.moIhqOnly, LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, .IhqOnly))

            RefreshPermissions()
        End With
    End Sub

    Private Sub RefreshPermissions()
        With Me.State.MyBO
            ElitaPlusPage.BindListControlToDataView(Me.lstSelectedPermission, .RolePermission.GetSelectedPermissions(), DALObjects.DALBase.COL_NAME_DESCRIPTION, DALObjects.DALBase.COL_NAME_ID, False)
            ElitaPlusPage.BindListControlToDataView(Me.lstAvailablePermission, .RolePermission.GetAvailablePermissions(), DALObjects.DALBase.COL_NAME_DESCRIPTION, DALObjects.DALBase.COL_NAME_ID, False)
        End With
    End Sub

    Sub EnableDisableFields()
        ControlMgr.SetEnableControl(Me, btnBack, True)
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnSave_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)

        'Now disable depebding on the object state
        If Me.State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
        End If

    End Sub

    Protected Sub CreateNew()
        Me.State.MyBO = New Role()
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
    End Sub

    Protected Sub PopulateBOsFormFrom()
        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO, "Code", Me.moRoleCode)
            Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.moRoleDescription)
            Me.PopulateBOProperty(Me.State.MyBO, "RoleProviderId", Me.moRoleProvider)
            Me.PopulateBOProperty(Me.State.MyBO, "IhqOnly", LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, New Guid(GetSelectedValue(Me.moIhqOnly))))
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

#End Region

End Class