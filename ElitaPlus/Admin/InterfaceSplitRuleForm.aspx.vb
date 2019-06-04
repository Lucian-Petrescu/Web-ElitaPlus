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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingParameters As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                Me.State.InterfaceSplitRuleReturnObject = CType(CallingParameters, InterfaceSplitRuleListForm.InterfaceSplitRuleReturnType)
                If (Not Me.State.InterfaceSplitRuleReturnObject.InterfaceSplitRuleId.Equals(Guid.Empty)) Then
                    Me.State.MyBO = New InterfaceSplitRule(Me.State.InterfaceSplitRuleReturnObject.InterfaceSplitRuleId)
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
                ' Popup Configuration for Delete Message Box
                lblCancelMessage.Text = TranslationBase.TranslateLabelOrMessage("MSG_CONFIRM_PROMPT")
                btnModalCancelYes.Attributes.Add("onclick", "YesButtonClick();")

                If (Me.State.MyBO Is Nothing) Then
                    Me.State.MyBO = New InterfaceSplitRule()
                End If

                PopulateDropDowns()

                ' Translate Grid Headers
                TranslateGridHeader(Me.ChildGrid)

                ' Populate Bread Crum
                UpdateBreadCrum()

                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            Else
                BindBoPropertiesToGridHeaders()
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
            'Me.PopulateBOsFormFrom()
            'Me.State.InterfaceSplitRuleReturnObject.InterfaceSplitRuleId = Me.State.MyBO.Id
            If Me.State.MyBO.IsFamilyDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New PageReturnType(Of InterfaceSplitRuleListForm.InterfaceSplitRuleReturnType)(ElitaPlusPage.DetailPageCommand.Back, Me.State.InterfaceSplitRuleReturnObject, Me.State.HasDataChanged))
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
            Me.State.MyBO.Save()
            'Me.State.MyBO = New InterfaceSplitRule(Me.State.MyBO.Id)
            Me.State.HasDataChanged = True
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
            Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New InterfaceSplitRule(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBOId.Equals(Guid.Empty) Then
                'It was a new with copy
                Me.State.MyBO = New InterfaceSplitRule(Me.State.ScreenSnapShotBOId)
            Else
                Me.State.MyBO = New InterfaceSplitRule()
            End If
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub DoDelete()
        Me.State.MyBO.Delete()
        Me.State.MyBO.Save()
        Me.State.HasDataChanged = True
        Me.State.InterfaceSplitRuleReturnObject.InterfaceSplitRuleId = Me.State.MyBO.Id
        Me.ReturnToCallingPage(New PageReturnType(Of InterfaceSplitRuleListForm.InterfaceSplitRuleReturnType)(ElitaPlusPage.DetailPageCommand.Delete, Me.State.InterfaceSplitRuleReturnObject, Me.State.HasDataChanged))
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
                    Me.ReturnToCallingPage(New PageReturnType(Of InterfaceSplitRuleListForm.InterfaceSplitRuleReturnType)(ElitaPlusPage.DetailPageCommand.Back, Me.State.InterfaceSplitRuleReturnObject, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New PageReturnType(Of InterfaceSplitRuleListForm.InterfaceSplitRuleReturnType)(ElitaPlusPage.DetailPageCommand.Back, Me.State.InterfaceSplitRuleReturnObject, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Delete
                    DoDelete()
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New PageReturnType(Of InterfaceSplitRuleListForm.InterfaceSplitRuleReturnType)(ElitaPlusPage.DetailPageCommand.Back, Me.State.InterfaceSplitRuleReturnObject, Me.State.HasDataChanged))
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

    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Source", Me.moSourceLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "SourceCode", Me.moSourceCodeLabel)
        'Me.BindBOPropertyToLabel(Me.State.MyBO, "NewSourceCode", Me.moDefaultNewSourceCodeLabel)
        Me.ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub PopulateDropDowns()
        'Me.BindListControlToDataView(moSourceDropDown, LookupListNew.DropdownLookupList(LookupListNew.LK_SOURCE, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
        Dim SourceList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="SOURCE",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

        Me.moSourceDropDown.Populate(SourceList.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })
    End Sub

    Protected Sub PopulateFormFromBOs()
        With Me.State.MyBO
            PopulateChildGrid()

            Me.PopulateControlFromBOProperty(Me.moSourceCode, .HeaderSourceCode)
            'Me.PopulateControlFromBOProperty(Me.moDefaultNewSourceCode, .DefaultNewSourceCode)
            Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim sourceDv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_SOURCE, languageId)
            Me.PopulateControlFromBOProperty(Me.moSourceDropDown, LookupListNew.GetIdFromCode(sourceDv, .HeaderSource))
            Me.PopulateControlFromBOProperty(Me.moSourceTextBox, LookupListNew.GetDescriptionFromCode(LookupListNew.LK_SOURCE, .HeaderSource, languageId))

            ControlMgr.SetVisibleControl(Me, moSourceDropDown, .IsNew)
            ControlMgr.SetVisibleControl(Me, moSourceTextBox, Not .IsNew)

        End With
    End Sub

    Private Sub BindBoPropertiesToGridHeaders()
        Dim childBo As New InterfaceSplitRule
        Me.BindBOPropertyToGridHeader(childBo, "FieldName", Me.ChildGrid.Columns(Me.GRID_COL_FIELD_NAME))
        Me.BindBOPropertyToGridHeader(childBo, "FieldOperator", Me.ChildGrid.Columns(Me.GRID_COL_FIELD_OPERATOR))
        Me.BindBOPropertyToGridHeader(childBo, "FieldValue", Me.ChildGrid.Columns(Me.GRID_COL_FIELD_VALUE))
        Me.BindBOPropertyToGridHeader(childBo, "NewSourceCode", Me.ChildGrid.Columns(Me.GRID_COL_NEW_SOURCE_CODE))

        Me.ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Protected Sub EnableDisableFields()
        If Me.State.IsChildEditing Then
            EnableDisableParentControls(False)
        Else
            EnableDisableParentControls(True)
        End If
    End Sub

    Sub EnableDisableParentControls(ByVal enableToggle As Boolean)
        ControlMgr.SetEnableControl(Me, btnBack, enableToggle)
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, btnSave_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, btnUndo_Write, enableToggle)
        ControlMgr.SetEnableControl(Me, btnAddNewRule, enableToggle)

        Me.moSourceCode.ReadOnly = Not (Me.State.MyBO.IsNew)
        'Me.moDefaultNewSourceCode.ReadOnly = Not (Me.State.MyBO.IsNew)
        ControlMgr.SetEnableControl(Me, moSourceDropDown, Me.State.MyBO.IsNew)

        If (enableToggle) Then
            'Enabled by Default
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)

            'Now disable depebding on the object state
            If Me.State.MyBO.IsNew Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            End If
        End If
    End Sub

    Protected Sub CreateNew()
        Me.State.ScreenSnapShotBOId = Guid.Empty 'Reset the backup copy
        Me.State.MyBO = New InterfaceSplitRule()
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
    End Sub

    Protected Sub PopulateBOsFormFrom()
        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO, "HeaderSourceCode", Me.moSourceCode)
            'Me.PopulateBOProperty(Me.State.MyBO, "DefaultNewSourceCode", Me.moDefaultNewSourceCode)
            Me.PopulateBOProperty(Me.State.MyBO, "HeaderSource", LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_SOURCE, ElitaPlusIdentity.Current.ActiveUser.LanguageId), GetSelectedItem(moSourceDropDown)))
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Private Sub PopulateChildGrid()
        Dim isEmpty As Boolean
        Dim dv As InterfaceSplitRule.InterfaceSplitRuleSelectionView = Me.State.MyBO.GetChildSelectionView()
        dv.Sort = Me.State.ChildSortExpression

        If (dv.Count > 0) Then
            Me.ChildGrid.PageSize = dv.Count
        Else
            Dim dr As DataRow
            dr = dv.Table.NewRow()
            dr(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_IS_NEW) = True
            dr(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_INTERFACE_SPLIT_RULE_ID) = Guid.NewGuid().ToByteArray()
            dv.Table.Rows.Add(dr)
            isEmpty = True
        End If

        SetPageAndSelectedIndexFromGuid(dv, Me.State.SelectedChildId, Me.ChildGrid, 1, Me.State.IsChildEditing)

        Me.ChildGrid.DataSource = dv
        Me.ChildGrid.AutoGenerateColumns = False
        Me.ChildGrid.DataBind()
        If (isEmpty) Then Me.ChildGrid.Rows(0).Visible = False
    End Sub

#End Region

#Region "Common Grid Events/Methods"

    Private Sub ChildGrid_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles ChildGrid.RowCreated
        Try
            MyBase.BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Rules Grid"
    Private Sub ChildGrid_DataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles ChildGrid.RowDataBound
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

                    moFieldNameDropDown = CType(e.Row.Cells(Me.GRID_COL_FIELD_NAME).FindControl(Me.GRID_COL_FIELD_NAME_DROPDOWN), DropDownList)
                    moFieldNameDropDown.Visible = True
                    moFieldOperatorDropDown = CType(e.Row.Cells(Me.GRID_COL_FIELD_OPERATOR).FindControl(Me.GRID_COL_FIELD_OPERATOR_DROPDOWN), DropDownList)
                    moFieldOperatorDropDown.Visible = True
                    moFieldValueTextBox = CType(e.Row.Cells(Me.GRID_COL_FIELD_VALUE).FindControl(Me.GRID_COL_FIELD_VALUE_TEXTBOX), TextBox)
                    moFieldValueTextBox.Visible = True
                    moNewSourceCodeTextBox = CType(e.Row.Cells(Me.GRID_COL_NEW_SOURCE_CODE).FindControl(Me.GRID_COL_NEW_SOURCE_CODE_TEXTBOX), TextBox)
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
                        Me.SetSelectedItemByText(moFieldNameDropDown, dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_FIELD_NAME).ToString())
                    End If

                    If (dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_FIELD_OPERATOR).ToString() <> String.Empty) Then
                        Me.SetSelectedItemByText(moFieldOperatorDropDown, dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_FIELD_OPERATOR).ToString())
                    End If

                    moFieldValueTextBox.Text = dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_FIELD_VALUE).ToString()
                    moNewSourceCodeTextBox.Text = dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_NEW_SOURCE_CODE).ToString()

                Else
                    moLabel = CType(e.Row.Cells(Me.GRID_COL_FIELD_NAME).FindControl(Me.GRID_COL_FIELD_NAME_LABEL), Label)
                    moLabel.Visible = True
                    moLabel.Text = dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_FIELD_NAME).ToString()

                    moLabel = CType(e.Row.Cells(Me.GRID_COL_FIELD_OPERATOR).FindControl(Me.GRID_COL_FIELD_OPERATOR_LABEL), Label)
                    moLabel.Visible = True
                    moLabel.Text = dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_FIELD_OPERATOR).ToString()

                    moLabel = CType(e.Row.Cells(Me.GRID_COL_FIELD_VALUE).FindControl(Me.GRID_COL_FIELD_VALUE_LABEL), Label)
                    moLabel.Visible = True
                    moLabel.Text = dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_FIELD_VALUE).ToString()

                    moLabel = CType(e.Row.Cells(Me.GRID_COL_NEW_SOURCE_CODE).FindControl(Me.GRID_COL_NEW_SOURCE_CODE_LABEL), Label)
                    moLabel.Visible = True
                    moLabel.Text = dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_NEW_SOURCE_CODE).ToString()

                    moImageButton = CType(e.Row.Cells(Me.GRID_COL_BUTTONS).FindControl(Me.GRID_COL_EDIT_IMAGE_BUTTON), ImageButton)
                    moImageButton.Visible = Not (Me.State.IsChildEditing)
                    moImageButton.CommandArgument = New Guid(DirectCast(dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_INTERFACE_SPLIT_RULE_ID), Byte())).ToString()
                    moImageButton = CType(e.Row.Cells(Me.GRID_COL_BUTTONS).FindControl(Me.GRID_COL_DELETE_IMAGE_BUTTON), ImageButton)
                    moImageButton.Visible = Not (Me.State.IsChildEditing)
                    moImageButton.CommandArgument = New Guid(DirectCast(dvRow(InterfaceSplitRule.InterfaceSplitRuleSelectionView.COL_NAME_INTERFACE_SPLIT_RULE_ID), Byte())).ToString()
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ChildGrid_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles ChildGrid.RowCommand
        Try
            Select Case e.CommandName
                Case EDIT_COMMAND_NAME
                    Me.State.SelectedChildId = New Guid(e.CommandArgument.ToString())
                    Me.BeginChildEdit()
                Case SAVE_COMMAND_NAME
                    PopulateChildBOsFormFrom()
                    Me.State.MyChildBO.Validate()
                    Me.State.MyChildBO.EndEdit()
                    Me.State.IsChildAdding = False
                    Me.State.IsChildEditing = False
                    Me.State.MyChildBO = Nothing
                Case CANCEL_COMMAND_NAME
                    If (Me.State.MyChildBO.IsNew) Then
                        If (Me.State.IsChildAdding) Then Me.State.MyChildBO.Delete()
                    Else
                        Me.State.MyChildBO.cancelEdit()
                    End If
                    Me.State.IsChildAdding = False
                    Me.State.IsChildEditing = False
                    Me.State.MyChildBO = Nothing
                Case DELETE_COMMAND_NAME
                    Me.State.MyChildBO = Me.State.MyBO.GetRuleChild(New Guid(e.CommandArgument.ToString()))
                    Me.State.MyChildBO.Delete()
                    Me.State.MyChildBO = Nothing
                Case Else
                    'TODO : Throw
            End Select
            Me.EnableDisableFields()
            PopulateChildGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Child Objects Command Buttons Event Handlers"

    Private Sub btnAddNewRule_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNewRule.Click
        Try
            Me.State.IsChildAdding = True
            Me.State.SelectedChildId = Guid.Empty
            Me.BeginChildEdit()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateChildBOsFormFrom()
        Dim moFieldNameDropDown As DropDownList
        Dim moFieldOperatorDropDown As DropDownList
        Dim moFieldValueTextBox As TextBox
        Dim moNewSourceCodeTextBox As TextBox
        With ChildGrid.Rows(ChildGrid.EditIndex)
            moFieldNameDropDown = CType(.Cells(Me.GRID_COL_FIELD_NAME).FindControl(Me.GRID_COL_FIELD_NAME_DROPDOWN), DropDownList)
            moFieldOperatorDropDown = CType(.Cells(Me.GRID_COL_FIELD_OPERATOR).FindControl(Me.GRID_COL_FIELD_OPERATOR_DROPDOWN), DropDownList)
            moFieldValueTextBox = CType(.Cells(Me.GRID_COL_FIELD_VALUE).FindControl(Me.GRID_COL_FIELD_VALUE_TEXTBOX), TextBox)
            moNewSourceCodeTextBox = CType(.Cells(Me.GRID_COL_NEW_SOURCE_CODE).FindControl(Me.GRID_COL_NEW_SOURCE_CODE_TEXTBOX), TextBox)

            Me.PopulateBOProperty(Me.State.MyChildBO, "FieldName", GetSelectedDescription(moFieldNameDropDown))
            Me.PopulateBOProperty(Me.State.MyChildBO, "FieldOperator", GetSelectedDescription(moFieldOperatorDropDown))
            Me.PopulateBOProperty(Me.State.MyChildBO, "FieldValue", moFieldValueTextBox.Text)
            Me.PopulateBOProperty(Me.State.MyChildBO, "NewSourceCode", moNewSourceCodeTextBox.Text)
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Private Sub BeginChildEdit()
        Me.State.IsChildEditing = True
        Me.EnableDisableFields()
        With Me.State
            If .SelectedChildId.Equals(Guid.Empty) Then
                .MyChildBO = .MyBO.GetNewRuleChild(Me.moSourceDropDown.SelectedItem.Text, Me.moSourceCode.Text)
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