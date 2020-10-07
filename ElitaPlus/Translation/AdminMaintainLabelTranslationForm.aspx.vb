Namespace Translation
    Partial Class AdminMaintainLabelTranslationForm
        Inherits ElitaPlusSearchPage
        Private Class PageStatus

            Public Sub New()
                pageIndex = 0
                pageCount = 0
            End Sub

        End Class

#Region "Member Variables"
        Private Shared pageIndex As Integer
        Private Shared pageCount As Integer
#End Region

#Region "Page State"
        Class MyState
            Public PageIndex As Integer = 0
            Public MyBO As DictionaryItem
            Public oDictItemTrans As DictItemTranslation
            Public oLabelExtended As Label_Extended
            Public DictItemId As Guid
            'Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public isNew As Boolean = False
            Public searchDV As DataView = Nothing
            Public SortExpression As String = DictItemTranslation.TranslationSearchDV.GRID_COL_ENGLISH
            'Public Canceling As Boolean
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            'Public LastErrMsg As String
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
                    State.MyBO = New DictionaryItem(CType(CallingParameters, Guid))
                Else
                    State.isNew = True
                    State.MyBO = New DictionaryItem
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

#End Region

#Region "Constants"

        Public Const URL As String = "AdminMaintainLabelTranslationForm.aspx"
        Public Const PAGETITLE As String = "LABEL TRANSLATION"
        Public Const PAGETAB As String = "ADMIN"
        Private Const GRID_COL_DICT_ITEM_ID As Integer = 0
        Private Const GRID_COL_TRANSLATION_ALIAS As Integer = 1
        Private Const GRID_COL_LANGUAGE_ALIAS As Integer = 2

        Private Const TRANSLATION_IN_GRID_CONTROL_NAME As String = "moTranslationTextGrid"
        Private Const LANGUAGE_IN_GRID_CONTROL_NAME As String = "moLanguageLabelGrid"

        'Private Const PAGE_SIZE As Integer = 10
        Private Const EMPTY As String = ""

        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED" '"The record has not been saved because the current record has not been changed"
        Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

#End Region
#Region "Handlers-Init"
        
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Try
                ErrControllerMaster.Clear_Hide()
                If Not Page.IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SetGridItemStyleColor(moDictionaryGrid)
                    'Me.MenuEnabled = False
                    If State.isNew Then
                        ControlMgr.SetVisibleControl(Me, moDictionaryGrid, False)
                    End If
                    PopulateTranslationGrid()
                    State.oLabelExtended = State.MyBO.AssociatedLabel(State.MyBO.Id, True, State.isNew)
                    PopulateFormFromBO()
                    State.PageIndex = 0
                Else
                    If State.searchDV IsNot Nothing Then
                        moDictionaryGrid.DataSource = State.searchDV
                    End If
                    If State.oLabelExtended Is Nothing Then
                        State.oLabelExtended = State.MyBO.AssociatedLabel(State.MyBO.Id, , True)
                    End If
                    BindBoPropertiesToGridHeaders()
                    CheckIfComingFromSaveConfirm()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

#End Region
#Region "Handlers"

#End Region
#Region "Controlling Logic"

        Private Sub ReturnFromEditing(Optional ByVal isSaveWithErrors As Boolean = False)

            If Not isSaveWithErrors Then

                moDictionaryGrid.EditItemIndex = NO_ROW_SELECTED_INDEX

                If (moDictionaryGrid.PageCount = 0) Then
                    'if returning to the "1st time in" screen
                    ControlMgr.SetVisibleControl(Me, moDictionaryGrid, False)
                Else
                    ControlMgr.SetVisibleControl(Me, moDictionaryGrid, True)
                End If

                State.IsEditMode = False
                PopulateTranslationGrid()
                If State.oLabelExtended.UiProgCode Is Nothing Then
                    Dim obj As Label_Extended = New Label_Extended(State.oLabelExtended.Id)
                    State.oLabelExtended.UiProgCode = obj.UiProgCode
                End If
                PopulateFormFromBO()
                State.PageIndex = moDictionaryGrid.CurrentPageIndex
                'SetButtonsState()
            End If
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As DataGrid, cellPosition As Integer, itemIndex As Integer, IsNewRowAndSingleCountry As Boolean)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim ctrlDealer As TextBox = CType(grid.Items(itemIndex).Cells(GRID_COL_TRANSLATION_ALIAS).FindControl(TRANSLATION_IN_GRID_CONTROL_NAME), TextBox)
            SetFocus(ctrlDealer)
            If cellPosition = GRID_COL_TRANSLATION_ALIAS Then
                SetFocus(ctrlDealer)
                ctrlDealer.Enabled = True
            Else
                Dim ctrlDescription As TextBox = CType(grid.Items(itemIndex).Cells(GRID_COL_LANGUAGE_ALIAS).FindControl(LANGUAGE_IN_GRID_CONTROL_NAME), TextBox)
                SetFocus(ctrlDescription)
                ctrlDealer.Enabled = False
            End If
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()

            BindBOPropertyToGridHeader(State.oDictItemTrans, "Translation", moDictionaryGrid.Columns(GRID_COL_TRANSLATION_ALIAS))
            BindBOPropertyToLabel(State.oLabelExtended, "UiProgCode", lblUiProgCode)
            ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub PopulateBOFromForm(bo As DictItemTranslation)
            Try
                Dim str As String = CType(moDictionaryGrid.Items(moDictionaryGrid.SelectedIndex).Cells(GRID_COL_TRANSLATION_ALIAS).FindControl(TRANSLATION_IN_GRID_CONTROL_NAME), TextBox).Text

                If State.isNew Then
                    str = txtUiProgCode.Text
                End If

                str = str.Replace("_", " ")

                PopulateBOProperty(bo, "Translation", str)
                If ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub PopulateFormFromBO()
            Try
                PopulateControlFromBOProperty(txtUiProgCode, State.oLabelExtended.UiProgCode)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub SetButtonsState()

            If (State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, moBtnSave_WRITE, True)
                ControlMgr.SetVisibleControl(Me, moBtnCancel, True)
                'Me.MenuEnabled = False
                If (cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
                SetGridControls(moDictionaryGrid, False)
            Else
                ControlMgr.SetVisibleControl(Me, moBtnSave_WRITE, False)
                ControlMgr.SetVisibleControl(Me, moBtnCancel, False)
                'Me.MenuEnabled = True
                If (cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If

        End Sub

        Private Sub CreateBoFromGrid(index As Integer)
            Dim DictionaryId As Guid
            Dim oLanguage As String

            moDictionaryGrid.SelectedIndex = index
            DictionaryId = New Guid(moDictionaryGrid.Items(index).Cells(GRID_COL_DICT_ITEM_ID).Text)
            'oLanguage = moDictionaryGrid.Items(index).Cells(Me.GRID_COL_LANGUAGE_ALIAS).ToString
            oLanguage = CType(moDictionaryGrid.Items(index).Cells(GRID_COL_LANGUAGE_ALIAS).FindControl(LANGUAGE_IN_GRID_CONTROL_NAME), Label).Text

            For i As Integer = 0 To State.searchDV.Table.Rows.Count - 1
                If Not State.isNew Then
                    Dim oDictItemId As Guid = New Guid(CType(State.searchDV.Table.Rows(i).Item(0), Byte()))
                    If DictionaryId.Equals(oDictItemId) Then
                        If Not State.searchDV.Table.Rows(i).Item(1).Equals(CType(moDictionaryGrid.Items(moDictionaryGrid.SelectedIndex).Cells(GRID_COL_TRANSLATION_ALIAS).FindControl(TRANSLATION_IN_GRID_CONTROL_NAME), TextBox).Text) Then
                            State.oDictItemTrans = State.MyBO.AssociatedTranslation(DictionaryId)
                            PopulateBOFromForm(State.oDictItemTrans)
                            State.oDictItemTrans.Save()
                        End If
                    End If
                Else
                    Dim Language As String = State.searchDV.Table.Rows(i).Item(2).ToString
                    If Language.ToUpper = oLanguage.ToUpper Then
                        State.oDictItemTrans = State.MyBO.AssociatedTranslation(Guid.Empty, State.isNew)
                        State.oDictItemTrans.LanguageId = New Guid(CType(State.searchDV.Table.Rows(i).Item(3), Byte()))
                        State.oDictItemTrans.DictItemId = State.MyBO.Id
                        PopulateBOFromForm(State.oDictItemTrans)
                        State.oDictItemTrans.Save()
                    End If
                End If
            Next

        End Sub

        Private Sub SavePage()
            Dim index As Integer = 0
            Dim totItems As Integer = moDictionaryGrid.Items.Count

            If State.isNew Then
                State.oLabelExtended.DictItemId = State.MyBO.Id
                PopulateBOProperty(State.oLabelExtended, "InUse", "Y")
            End If

            PopulateBOProperty(State.oLabelExtended, "UiProgCode", txtUiProgCode.Text.ToUpper)
            State.oLabelExtended.Save()

            For index = 0 To totItems - 1
                CreateBoFromGrid(index)
            Next

        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()

            Dim confResponse As String = HiddenSavePagePromptResponse.Value

            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    SavePage()
                    If (State.MyBO.IsFamilyDirty) Then
                        State.MyBO.Save()
                    End If
                End If
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(State.MyBO.Id)
                    Case ElitaPlusPage.DetailPageCommand.New_
                        'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(State.MyBO.Id)
                    Case ElitaPlusPage.DetailPageCommand.New_
                        'Me.State.isNew = True
                        'Me.PopulateGrid()
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        'Me.State.isNew = False
                        'Me.State.year = Me.GetSelectedDescription(Me.moYearDropDownList)
                        'PopulateYearsDropdown()
                        'setYearSelection(Me.State.year)
                        'PopulateGrid()
                End Select
            End If

            If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Undo Then
                'Me.State.year = Me.State.prevSelectedYear
                'PopulateYearsDropdown()
                'setYearSelection(Me.State.year)
                Return
            End If

            'Clean after consuming the action
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenSavePagePromptResponse.Value = ""

        End Sub

#End Region

#Region "Private Methods"


#End Region
#Region "Button Management"

        Private Sub moBtnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles moBtnSave_WRITE.Click
            Try
                SavePage()
                If (State.MyBO.IsFamilyDirty) Then
                    State.MyBO.Save()
                    State.isNew = False
                    ShowInfoMsgBox(MSG_RECORD_SAVED_OK)
                    State.searchDV = Nothing
                    PopulateTranslationGrid()
                    PopulateFormFromBO()
                    ControlMgr.SetVisibleControl(Me, moDictionaryGrid, True)
                Else
                    ShowInfoMsgBox(MSG_RECORD_NOT_SAVED)
                    ReturnFromEditing()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
                ReturnFromEditing(True)
                If State.isNew Then
                    State.MyBO = Nothing
                    State.MyBO = New DictionaryItem
                    State.oLabelExtended = Nothing
                    State.searchDV = Nothing
                    State.searchDV = State.MyBO.GetLanguageList()
                    PopulateTranslationGrid()
                End If
            End Try

        End Sub

        Private Sub moBtnCancel_Click(sender As System.Object, e As System.EventArgs) Handles moBtnCancel.Click
            Try
                moDictionaryGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                'Me.State.Canceling = True
                ReturnFromEditing()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub


        Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Try
                SavePage()
                If State.MyBO.IsFamilyDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    ReturnToCallingPage(State.MyBO.Id)
                End If

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
                'Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                'Me.State.LastErrMsg = Me.ErrControllerMaster.Text
            End Try
        End Sub


        Private Sub btnNew_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            State.searchDV = Nothing
            State.searchDV = State.MyBO.GetLanguageList()

            PopulateTranslationGrid()
        End Sub

#End Region

#Region " Datagrid Related "

        Private Sub PopulateTranslationGrid()
            Dim dv As DataView
            Try
                If (State.searchDV Is Nothing) Then
                    'Me.SetStateProperties()
                    State.searchDV = GetTranslationGridDataView()
                End If

                State.searchDV.Sort = State.SortExpression

                moDictionaryGrid.AutoGenerateColumns = False
                moDictionaryGrid.Columns(GRID_COL_TRANSLATION_ALIAS).SortExpression = State.oDictItemTrans.TranslationSearchDV.GRID_COL_TRANSLATION
                moDictionaryGrid.Columns(GRID_COL_LANGUAGE_ALIAS).SortExpression = State.oDictItemTrans.TranslationSearchDV.GRID_COL_ENGLISH.ToUpper

                SetPageAndSelectedIndexFromGuid(State.searchDV, State.DictItemId, moDictionaryGrid, State.PageIndex)
                SortAndBindGrid()

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub SortAndBindGrid()
            State.PageIndex = moDictionaryGrid.CurrentPageIndex
            TranslateGridControls(moDictionaryGrid)
            moDictionaryGrid.DataSource = State.searchDV
            HighLightSortColumn(moDictionaryGrid, State.SortExpression)
            moDictionaryGrid.DataBind()

            'ControlMgr.SetVisibleControl(Me, moDictionaryGrid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, moDictionaryGrid.Visible)

            Session("recCount") = State.searchDV.Count

            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moDictionaryGrid)
        End Sub

        Private Function GetTranslationGridDataView() As DataView
            With State
                If Not State.isNew Then
                    State.searchDV = State.oDictItemTrans.GetTranslationsList(State.MyBO.Id)
                Else
                    State.searchDV = State.MyBO.GetLanguageList()
                End If
            End With

            State.searchDV.Sort = moDictionaryGrid.DataMember()
            moDictionaryGrid.PageSize = State.searchDV.Count
            moDictionaryGrid.DataSource = State.searchDV

            Return (State.searchDV)
        End Function

        Private Sub moDictionaryGrid_PageIndexChanged(source As System.Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDictionaryGrid.PageIndexChanged
            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    moDictionaryGrid.CurrentPageIndex = State.PageIndex
                    PopulateTranslationGrid()
                    moDictionaryGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                moDictionaryGrid.CurrentPageIndex = NewCurrentPageIndex(moDictionaryGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                PopulateTranslationGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moDictionaryGrid.ItemDataBound
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim str As String
            Dim oTextBox As TextBox
            Dim oLabel As Label
            
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                With e.Item
                    PopulateControlFromBOProperty(.Cells(GRID_COL_DICT_ITEM_ID), dvRow(DictItemTranslation.TranslationSearchDV.GRID_COL_ID))
                    oTextBox = CType(e.Item.Cells(GRID_COL_TRANSLATION_ALIAS).FindControl(TRANSLATION_IN_GRID_CONTROL_NAME), TextBox)
                    If e.Item.ItemIndex() = 0 Then
                        SetFocus(oTextBox)
                    End If
                    PopulateControlFromBOProperty(oTextBox, dvRow(DictItemTranslation.TranslationSearchDV.GRID_COL_TRANSLATION))
                    PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_LANGUAGE_ALIAS).FindControl(LANGUAGE_IN_GRID_CONTROL_NAME), dvRow(DictItemTranslation.TranslationSearchDV.GRID_COL_ENGLISH).ToString.ToUpper)
                    oLabel = CType(e.Item.Cells(GRID_COL_LANGUAGE_ALIAS).FindControl(LANGUAGE_IN_GRID_CONTROL_NAME), Label)

                End With
            End If
            BaseItemBound(sender, e)

        End Sub

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moDictionaryGrid.SortCommand
            Try
                If State.SortExpression.StartsWith(e.SortExpression) Then
                    If State.SortExpression.EndsWith(" DESC") Then
                        State.SortExpression = e.SortExpression
                    Else
                        State.SortExpression &= " DESC"
                    End If
                Else
                    State.SortExpression = e.SortExpression
                End If
                'To handle the requirement of always going to the FIRST page on the Grid whenever the user switches the sorting criterion
                'Set the Me.State.selectedClaimId = Guid.Empty and set Me.State.PageIndex = 0
                State.DictItemId = Guid.Empty
                State.PageIndex = 0

                PopulateTranslationGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        'The pencil or the trash icon was clicked
        Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)

        End Sub

        Public Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

#End Region

        Private Sub ShowInfoMsgBox(strMsg As String, Optional ByVal Translate As Boolean = True)
            Dim translatedMsg As String = strMsg
            If Translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strMsg)
            Dim sJavaScript As String
            sJavaScript = "<SCRIPT>" & Environment.NewLine
            sJavaScript &= "setTimeout(""showMessage('" & translatedMsg & "', '" & "AlertWindow" & "', '" & MSG_BTN_OK & "', '" & MSG_TYPE_INFO & "', '" & "null" & "')"", 0);" & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine
            RegisterStartupScript("ShowConfirmation", sJavaScript)
        End Sub

    End Class

End Namespace

