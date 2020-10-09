Imports Assurant.ElitaPlus.DALObjects
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Public Class InterfaceSplitRuleForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "


    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Const PAGETAB As String = "Admin"
    Public Const PAGETITLE As String = "INTERFACE_SPLIT_RULES"
    Public Const URL As String = "~/Admin/InterfaceSplitRuleForm.aspx"

    Private Const GRID_COL_FIELD_NAME As Integer = 0
    Private Const GRID_COL_FIELD_OPERATOR As Integer = 1
    Private Const GRID_COL_FIELD_VALUE As Integer = 2
    Private Const GRID_COL_NEW_SOURCE_CODE As Integer = 3
    Private Const GRID_COL_BUTTONS As Integer = 4

    Private Const GRID_COL_FIELD_NAME_DROPDOWN As String = "moFieldNameDropDown"
    Private Const GRID_COL_FIELD_OPERATOR_DROPDOWN As String = "moOperatorDropDown"
    Private Const GRID_COL_FIELD_VALUE_TEXTBOX As String = "moFieldValueTextBox"
    Private Const GRID_COL_NEW_SOURCE_CODE_TEXTBOX As String = "moSourceCodeTextBox"

    Private Const GRID_COL_FIELD_NAME_LABEL As String = "moFieldName"
    Private Const GRID_COL_FIELD_OPERATOR_LABEL As String = "moOperator"
    Private Const GRID_COL_FIELD_VALUE_LABEL As String = "moFieldValue"
    Private Const GRID_COL_NEW_SOURCE_CODE_LABEL As String = "moSourceCode"

    Private Const GRID_COL_EDIT_IMAGE_BUTTON As String = "moEdit"
    Private Const GRID_COL_DELETE_IMAGE_BUTTON As String = "moDelete"
#End Region

#Region "Page State"

    Class MyState
        Public MyBO As InterfaceSplitRule
        Public ScreenSnapShotBOId As Guid
        Public MyChildBO As InterfaceSplitRule
        Public IsReadOnly As Boolean

        Public IsChildEditing As Boolean = False
        Public IsChildAdding As Boolean = False

        Public ChildSortExpression As String = InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_FIELD_VALUE


        Public SelectedChildId As Guid = Guid.Empty

        Public InterfaceSplitRuleReturnObject As InterfaceSplitRuleListForm.InterfaceSplitRuleReturnType
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
                State.InterfaceSplitRuleReturnObject = CType(CallingParameters, InterfaceSplitRuleListForm.InterfaceSplitRuleReturnType)
                If (Not State.InterfaceSplitRuleReturnObject.InterfaceSplitRuleId.Equals(Guid.Empty)) Then
                    State.MyBO = New InterfaceSplitRule(State.InterfaceSplitRuleReturnObject.InterfaceSplitRuleId)
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
                ' Popup Configuration for Delete Message Box
                lblCancelMessage.Text = TranslationBase.TranslateLabelOrMessage("MSG_CONFIRM_PROMPT")
                btnModalCancelYes.Attributes.Add("onclick", "YesButtonClick();")

                If (State.MyBO Is Nothing) Then
                    State.MyBO = New InterfaceSplitRule()
                End If

                PopulateDropDowns()

                ' Translate Grid Headers
                TranslateGridHeader(ChildGrid)

                ' Populate Bread Crum
                UpdateBreadCrum()

                PopulateFormFromBOs()
                EnableDisableFields()
            Else
                BindBoPropertiesToGridHeaders()
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
            'Me.PopulateBOsFormFrom()
            'Me.State.InterfaceSplitRuleReturnObject.InterfaceSplitRuleId = Me.State.MyBO.Id
            If State.MyBO.IsFamilyDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New PageReturnType(Of InterfaceSplitRuleListForm.InterfaceSplitRuleReturnType)(ElitaPlusPage.DetailPageCommand.Back, State.InterfaceSplitRuleReturnObject, State.HasDataChanged))
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
            State.MyBO.Save()
            'Me.State.MyBO = New InterfaceSplitRule(Me.State.MyBO.Id)
            State.HasDataChanged = True
            PopulateFormFromBOs()
            EnableDisableFields()
            MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New InterfaceSplitRule(State.MyBO.Id)
            ElseIf Not State.ScreenSnapShotBOId.Equals(Guid.Empty) Then
                'It was a new with copy
                State.MyBO = New InterfaceSplitRule(State.ScreenSnapShotBOId)
            Else
                State.MyBO = New InterfaceSplitRule()
            End If
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub DoDelete()
        State.MyBO.Delete()
        State.MyBO.Save()
        State.HasDataChanged = True
        State.InterfaceSplitRuleReturnObject.InterfaceSplitRuleId = State.MyBO.Id
        ReturnToCallingPage(New PageReturnType(Of InterfaceSplitRuleListForm.InterfaceSplitRuleReturnType)(ElitaPlusPage.DetailPageCommand.Delete, State.InterfaceSplitRuleReturnObject, State.HasDataChanged))
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
                    ReturnToCallingPage(New PageReturnType(Of InterfaceSplitRuleListForm.InterfaceSplitRuleReturnType)(ElitaPlusPage.DetailPageCommand.Back, State.InterfaceSplitRuleReturnObject, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ReturnToCallingPage(New PageReturnType(Of InterfaceSplitRuleListForm.InterfaceSplitRuleReturnType)(ElitaPlusPage.DetailPageCommand.Back, State.InterfaceSplitRuleReturnObject, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Delete
                    DoDelete()
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New PageReturnType(Of InterfaceSplitRuleListForm.InterfaceSplitRuleReturnType)(ElitaPlusPage.DetailPageCommand.Back, State.InterfaceSplitRuleReturnObject, State.HasDataChanged))
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

    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, "Source", moSourceLabel)
        BindBOPropertyToLabel(State.MyBO, "SourceCode", moSourceCodeLabel)
        'Me.BindBOPropertyToLabel(Me.State.MyBO, "NewSourceCode", Me.moDefaultNewSourceCodeLabel)
        ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub PopulateDropDowns()
        'Me.BindListControlToDataView(moSourceDropDown, LookupListNew.DropdownLookupList(LookupListNew.LK_SOURCE, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
        Dim SourceList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="SOURCE",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

        moSourceDropDown.Populate(SourceList.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })
    End Sub

    Protected Sub PopulateFormFromBOs()
        With State.MyBO
            PopulateChildGrid()

            PopulateControlFromBOProperty(moSourceCode, .HeaderSourceCode)
            'Me.PopulateControlFromBOProperty(Me.moDefaultNewSourceCode, .DefaultNewSourceCode)
            Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim sourceDv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_SOURCE, languageId)
            PopulateControlFromBOProperty(moSourceDropDown, LookupListNew.GetIdFromCode(sourceDv, .HeaderSource))
            PopulateControlFromBOProperty(moSourceTextBox, LookupListNew.GetDescriptionFromCode(LookupListNew.LK_SOURCE, .HeaderSource, languageId))

            ControlMgr.SetVisibleControl(Me, moSourceDropDown, .IsNew)
            ControlMgr.SetVisibleControl(Me, moSourceTextBox, Not .IsNew)

        End With
    End Sub

    Private Sub BindBoPropertiesToGridHeaders()
        Dim childBo As New InterfaceSplitRule
        BindBOPropertyToGridHeader(childBo, "FieldName", ChildGrid.Columns(GRID_COL_FIELD_NAME))
        BindBOPropertyToGridHeader(childBo, "FieldOperator", ChildGrid.Columns(GRID_COL_FIELD_OPERATOR))
        BindBOPropertyToGridHeader(childBo, "FieldValue", ChildGrid.Columns(GRID_COL_FIELD_VALUE))
        BindBOPropertyToGridHeader(childBo, "NewSourceCode", ChildGrid.Columns(GRID_COL_NEW_SOURCE_CODE))

        ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Protected Sub EnableDisableFields()
        If State.IsChildEditing Then
            EnableDisableParentControls(False)
        Else
            EnableDisableParentControls(True)
        End If
    End Sub

    Sub EnableDisableParentControls(enableToggle As Boolean)
        ControlMgr.SetEnableControl(Me, btnBack, enableToggle)
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, btnSave_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, btnUndo_Write, enableToggle)
        ControlMgr.SetEnableControl(Me, btnAddNewRule, enableToggle)

        moSourceCode.ReadOnly = Not (State.MyBO.IsNew)
        'Me.moDefaultNewSourceCode.ReadOnly = Not (Me.State.MyBO.IsNew)
        ControlMgr.SetEnableControl(Me, moSourceDropDown, State.MyBO.IsNew)

        If (enableToggle) Then
            'Enabled by Default
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)

            'Now disable depebding on the object state
            If State.MyBO.IsNew Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            End If
        End If
    End Sub

    Protected Sub CreateNew()
        State.ScreenSnapShotBOId = Guid.Empty 'Reset the backup copy
        State.MyBO = New InterfaceSplitRule()
        PopulateFormFromBOs()
        EnableDisableFields()
    End Sub

    Protected Sub PopulateBOsFormFrom()
        With State.MyBO
            PopulateBOProperty(State.MyBO, "HeaderSourceCode", moSourceCode)
            'Me.PopulateBOProperty(Me.State.MyBO, "DefaultNewSourceCode", Me.moDefaultNewSourceCode)
            PopulateBOProperty(State.MyBO, "HeaderSource", LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_SOURCE, ElitaPlusIdentity.Current.ActiveUser.LanguageId), GetSelectedItem(moSourceDropDown)))
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Private Sub PopulateChildGrid()
        Dim isEmpty As Boolean
        Dim dv As InterfaceSplitRule.InterfaceSplitRuleSelectionView = State.MyBO.GetChildSelectionView()
        dv.Sort = State.ChildSortExpression

        If (dv.Count > 0) Then
            ChildGrid.PageSize = dv.Count
        Else
            Dim dr As DataRow
            dr = dv.Table.NewRow()
            dr(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_IS_NEW) = True
            dr(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_INTERFACE_SPLIT_RULE_ID) = Guid.NewGuid().ToByteArray()
            dv.Table.Rows.Add(dr)
            isEmpty = True
        End If

        SetPageAndSelectedIndexFromGuid(dv, State.SelectedChildId, ChildGrid, 1, State.IsChildEditing)

        ChildGrid.DataSource = dv
        ChildGrid.AutoGenerateColumns = False
        ChildGrid.DataBind()
        If (isEmpty) Then ChildGrid.Rows(0).Visible = False
    End Sub

#End Region

#Region "Common Grid Events/Methods"

    Private Sub ChildGrid_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles ChildGrid.RowCreated
        Try
            MyBase.BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Rules Grid"
    Private Sub ChildGrid_DataBound(sender As Object, e As GridViewRowEventArgs) Handles ChildGrid.RowDataBound
        Try
            Dim itemType As DataControlRowType = e.Row.RowType
            Dim moLabel As Label
            Dim moFieldNameDropDown As DropDownList
            Dim moFieldOperatorDropDown As DropDownList
            Dim moFieldValueTextBox As TextBox
            Dim moNewSourceCodeTextBox As TextBox
            Dim moImageButton As ImageButton
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim isNewRow As Boolean
            If itemType = DataControlRowType.DataRow Then
                If ((e.Row.RowState And DataControlRowState.Edit) = DataControlRowState.Edit) Then
                    isNewRow = DirectCast(dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_IS_NEW), Boolean)

                    moFieldNameDropDown = CType(e.Row.Cells(GRID_COL_FIELD_NAME).FindControl(GRID_COL_FIELD_NAME_DROPDOWN), DropDownList)
                    moFieldNameDropDown.Visible = True
                    moFieldOperatorDropDown = CType(e.Row.Cells(GRID_COL_FIELD_OPERATOR).FindControl(GRID_COL_FIELD_OPERATOR_DROPDOWN), DropDownList)
                    moFieldOperatorDropDown.Visible = True
                    moFieldValueTextBox = CType(e.Row.Cells(GRID_COL_FIELD_VALUE).FindControl(GRID_COL_FIELD_VALUE_TEXTBOX), TextBox)
                    moFieldValueTextBox.Visible = True
                    moNewSourceCodeTextBox = CType(e.Row.Cells(GRID_COL_NEW_SOURCE_CODE).FindControl(GRID_COL_NEW_SOURCE_CODE_TEXTBOX), TextBox)
                    moNewSourceCodeTextBox.Visible = True

                    'Dim fieldNameDV As DataView = LookupListNew.GetFieldNames()
                    'Dim sourceDataView As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_SOURCE, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    'fieldNameDV.RowFilter = "SOURCE = '" & LookupListNew.GetCodeFromId(sourceDataView, GetSelectedItem(moSourceDropDown)) & "'"
                    'ElitaPlusPage.BindListControlToDataView(moFieldNameDropDown, fieldNameDV)


                    Dim SourceList As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:="SOURCE",
                                                                            languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

                    Dim fieldName As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:="InterfaceSplitColumns",
                                                                            languageCode:=Thread.CurrentPrincipal.GetLanguageCode())



                    Dim filterCode As String = (From Source In SourceList
                                                Where Source.ListItemId = GetSelectedItem(moSourceDropDown)
                                                Select Source.Code).FirstOrDefault()

                    Dim filteredFieldNameList As DataElements.ListItem() = (From Source In fieldName
                                                                            Where Source.Code = filterCode
                                                                            Select Source).ToArray()

                    moFieldNameDropDown.Populate(filteredFieldNameList.ToArray(),
                                                    New PopulateOptions() With
                                                    {
                                                        .AddBlankItem = True
                                                    })

                    'Dim fieldOperatorDV As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_OPERATORS, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    'ElitaPlusPage.BindListControlToDataView(moFieldOperatorDropDown, fieldOperatorDV)


                    Dim OperatorList As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:="OPERATORS",
                                                                            languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

                    moFieldOperatorDropDown.Populate(OperatorList.ToArray(),
                                            New PopulateOptions() With
                                            {
                                                .AddBlankItem = True
                                            })

                    If (dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_FIELD_NAME).ToString() <> String.Empty) Then
                        SetSelectedItemByText(moFieldNameDropDown, dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_FIELD_NAME).ToString())
                    End If

                    If (dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_FIELD_OPERATOR).ToString() <> String.Empty) Then
                        SetSelectedItemByText(moFieldOperatorDropDown, dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_FIELD_OPERATOR).ToString())
                    End If

                    moFieldValueTextBox.Text = dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_FIELD_VALUE).ToString()
                    moNewSourceCodeTextBox.Text = dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_NEW_SOURCE_CODE).ToString()

                Else
                    moLabel = CType(e.Row.Cells(GRID_COL_FIELD_NAME).FindControl(GRID_COL_FIELD_NAME_LABEL), Label)
                    moLabel.Visible = True
                    moLabel.Text = dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_FIELD_NAME).ToString()

                    moLabel = CType(e.Row.Cells(GRID_COL_FIELD_OPERATOR).FindControl(GRID_COL_FIELD_OPERATOR_LABEL), Label)
                    moLabel.Visible = True
                    moLabel.Text = dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_FIELD_OPERATOR).ToString()

                    moLabel = CType(e.Row.Cells(GRID_COL_FIELD_VALUE).FindControl(GRID_COL_FIELD_VALUE_LABEL), Label)
                    moLabel.Visible = True
                    moLabel.Text = dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_FIELD_VALUE).ToString()

                    moLabel = CType(e.Row.Cells(GRID_COL_NEW_SOURCE_CODE).FindControl(GRID_COL_NEW_SOURCE_CODE_LABEL), Label)
                    moLabel.Visible = True
                    moLabel.Text = dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_NEW_SOURCE_CODE).ToString()

                    moImageButton = CType(e.Row.Cells(GRID_COL_BUTTONS).FindControl(GRID_COL_EDIT_IMAGE_BUTTON), ImageButton)
                    moImageButton.Visible = Not (State.IsChildEditing)
                    moImageButton.CommandArgument = New Guid(DirectCast(dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_INTERFACE_SPLIT_RULE_ID), Byte())).ToString()
                    moImageButton = CType(e.Row.Cells(GRID_COL_BUTTONS).FindControl(GRID_COL_DELETE_IMAGE_BUTTON), ImageButton)
                    moImageButton.Visible = Not (State.IsChildEditing)
                    moImageButton.CommandArgument = New Guid(DirectCast(dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_INTERFACE_SPLIT_RULE_ID), Byte())).ToString()
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ChildGrid_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles ChildGrid.RowCommand
        Try
            Select Case e.CommandName
                Case EDIT_COMMAND_NAME
                    State.SelectedChildId = New Guid(e.CommandArgument.ToString())
                    BeginChildEdit()
                Case SAVE_COMMAND_NAME
                    PopulateChildBOsFormFrom()
                    State.MyChildBO.Validate()
                    State.MyChildBO.EndEdit()
                    State.IsChildAdding = False
                    State.IsChildEditing = False
                    State.MyChildBO = Nothing
                Case CANCEL_COMMAND_NAME
                    If (State.MyChildBO.IsNew) Then
                        If (State.IsChildAdding) Then State.MyChildBO.Delete()
                    Else
                        State.MyChildBO.cancelEdit()
                    End If
                    State.IsChildAdding = False
                    State.IsChildEditing = False
                    State.MyChildBO = Nothing
                Case DELETE_COMMAND_NAME
                    State.MyChildBO = State.MyBO.GetRuleChild(New Guid(e.CommandArgument.ToString()))
                    State.MyChildBO.Delete()
                    State.MyChildBO = Nothing
                Case Else
                    'TODO : Throw
            End Select
            EnableDisableFields()
            PopulateChildGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Child Objects Command Buttons Event Handlers"

    Private Sub btnAddNewRule_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnAddNewRule.Click
        Try
            State.IsChildAdding = True
            State.SelectedChildId = Guid.Empty
            BeginChildEdit()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateChildBOsFormFrom()
        Dim moFieldNameDropDown As DropDownList
        Dim moFieldOperatorDropDown As DropDownList
        Dim moFieldValueTextBox As TextBox
        Dim moNewSourceCodeTextBox As TextBox
        With ChildGrid.Rows(ChildGrid.EditIndex)
            moFieldNameDropDown = CType(.Cells(GRID_COL_FIELD_NAME).FindControl(GRID_COL_FIELD_NAME_DROPDOWN), DropDownList)
            moFieldOperatorDropDown = CType(.Cells(GRID_COL_FIELD_OPERATOR).FindControl(GRID_COL_FIELD_OPERATOR_DROPDOWN), DropDownList)
            moFieldValueTextBox = CType(.Cells(GRID_COL_FIELD_VALUE).FindControl(GRID_COL_FIELD_VALUE_TEXTBOX), TextBox)
            moNewSourceCodeTextBox = CType(.Cells(GRID_COL_NEW_SOURCE_CODE).FindControl(GRID_COL_NEW_SOURCE_CODE_TEXTBOX), TextBox)

            PopulateBOProperty(State.MyChildBO, "FieldName", GetSelectedDescription(moFieldNameDropDown))
            PopulateBOProperty(State.MyChildBO, "FieldOperator", GetSelectedDescription(moFieldOperatorDropDown))
            PopulateBOProperty(State.MyChildBO, "FieldValue", moFieldValueTextBox.Text)
            PopulateBOProperty(State.MyChildBO, "NewSourceCode", moNewSourceCodeTextBox.Text)
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Private Sub BeginChildEdit()
        State.IsChildEditing = True
        EnableDisableFields()
        With State
            If .SelectedChildId.Equals(Guid.Empty) Then
                .MyChildBO = .MyBO.GetNewRuleChild(moSourceDropDown.SelectedItem.Text, moSourceCode.Text)
                .SelectedChildId = .MyChildBO.Id
            Else
                .MyChildBO = .MyBO.GetRuleChild(.SelectedChildId)
            End If
            .MyChildBO.BeginEdit()
        End With
        PopulateChildGrid()
    End Sub
#End Region

End Class